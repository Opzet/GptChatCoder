using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAss
{
    public static class RepoToGpt
    {
        //The purpose of both values is to ensure that the content being processed fits within the token limit(4096 tokens) enforced by GPT-3.
        public const int MAX_TEXT_LENGTH = 2048;  // Set to ChatGPT's API limit or desired value.
        private static int currentFileNumber = 1;
        private static string buffer = "";

        private static int FindLastFunctionEnd(string code, int maxIndex)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            // Consider various "function-like" constructs
            var functionNodes = root.DescendantNodes().Where(node =>
                node is MethodDeclarationSyntax ||
                node is ConstructorDeclarationSyntax ||
                node is DestructorDeclarationSyntax ||
                node is PropertyDeclarationSyntax ||
                node is DelegateDeclarationSyntax
            ).ToList();

            int lastFunctionEnd = -1;

            foreach (var functionNode in functionNodes)
            {
                var span = functionNode.Span;
                if (span.End <= maxIndex)
                {
                    lastFunctionEnd = span.End;
                }
                else
                {
                    break;  // Once we cross the maxIndex, we can break out of the loop.
                }
            }

            return lastFunctionEnd;
        }

        static int totalChunks = 0; // Initialize totalChunksAcrossFiles counter


        public static int TraverseAndSaveChunks(string path, string outputDirectory, int uploadLimit = MAX_TEXT_LENGTH)
        {
            foreach (var item in Directory.GetFileSystemEntries(path))
            {
                if (File.Exists(item))
                {
                    if (IsRequiredFileExtension(Path.GetExtension(item).ToLower()))
                    {

                        string relativePath = GetRelativePath(item, path);
                        string content = File.ReadAllText(item, Encoding.Default);
                        content = ProcessContent(content);
                        content = RemoveBlankLinesAndWhitespace(content);

                        // Split the content into chunks and save them to the output directory
                        buffer += content;

                        while (buffer.Length >= uploadLimit)
                        {
                            int splitIndex = FindLastFunctionEnd(buffer, uploadLimit);
                            if (splitIndex == -1)
                            {
                                // Handle cases where a suitable split isn't found.
                                splitIndex = uploadLimit; // Use default split
                            }

                            string toWrite = buffer.Substring(0, splitIndex + 1); // +1 to include the last brace
                            File.WriteAllText(Path.Combine(outputDirectory, $"chunk_{currentFileNumber}.txt"), toWrite);
                            buffer = buffer.Substring(splitIndex + 1);

                            currentFileNumber++;
                        }

                        //Write last under sized part
                        if (buffer.Length > 0)
                            File.WriteAllText(Path.Combine(outputDirectory, $"chunk_{currentFileNumber}.txt"), buffer);
                        else
                            currentFileNumber--;

                        buffer = "";
                        
                    }
                }
            }
            totalChunks = currentFileNumber-1;
            return totalChunks; //Zer referenced
        }


        private static int CalculateChunkSize(string content, int uploadLimit)
        {
            // Calculate a chunk size that respects the upload limit and doesn't exceed content length
            return Math.Min(uploadLimit, content.Length);
        }


        public static string ReadandWrapChunks(string file)
        {
            string formattedContent = "";

            string fileName = Path.GetFileNameWithoutExtension(file);
            if (fileName.StartsWith("chunk_") && int.TryParse(fileName.Substring(6), out int chunkNumber))
            {
                // Read chunk content from the file
                string chunkContent = File.ReadAllText(file, Encoding.Default);

                // Create formatted content
                formattedContent = $"[CHUNK] \r\nChunk Number: {chunkNumber} \r\nTotal Chunks: {totalChunks} \r\nContent: {chunkContent} \r\n[/CHUNK]\r\n";
            }

            return formattedContent;
        }


        private static bool IsRequiredFileExtension(string extension)
        {
            string[] requiredExtensions = { ".cs", ".config", ".csproj", ".sln", ".resx", ".Designer.cs" };
            return requiredExtensions.Contains(extension.ToLower());
        }

        private static string GetRelativePath(string fullPath, string basePath)
        {
            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);
            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        private static string ProcessContent(string content)
        {
            content = Regex.Replace(content, @"//.*$", "", RegexOptions.Multiline);
            content = Regex.Replace(content, @"/\*.*?\*/", "", RegexOptions.Singleline);
            content = Regex.Replace(content, @"#region.*?#endregion", "", RegexOptions.Singleline);
            content = Regex.Replace(content, @"#if.*?#endif", "", RegexOptions.Singleline);
            return content;
        }

        private static string RemoveBlankLinesAndWhitespace(string content)
        {
            string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> nonEmptyLines = new List<string>();

            bool insideCommentBlock = false;
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("/*"))
                {
                    insideCommentBlock = true;
                    nonEmptyLines.Add(trimmedLine);
                }
                else if (insideCommentBlock && trimmedLine.EndsWith("*/"))
                {
                    insideCommentBlock = false;
                    nonEmptyLines[nonEmptyLines.Count - 1] += " " + trimmedLine;
                }
                else if (!insideCommentBlock && !string.IsNullOrWhiteSpace(trimmedLine))
                {
                    nonEmptyLines.Add(trimmedLine);
                }
            }

            return string.Join(Environment.NewLine, nonEmptyLines);
        }

    }
}
