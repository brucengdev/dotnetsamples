using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

public class PipeAndPipelines
{
    private static async Task ProcessData(string input)
    {
        var pipe = new Pipe();
        var readTask = ReadAndProcessLines(pipe.Reader);
        var writeTask = WriteToPipe(input, pipe.Writer);
        await Task.WhenAll(readTask, writeTask);
    }

    private static async Task ReadAndProcessLines(PipeReader reader)
    {
        var lineNumber = 0;
        while (true)
        {
            ReadResult result = await reader.ReadAsync();
            ReadOnlySequence<byte> buffer = result.Buffer;

            while(TryReadLine(ref buffer, out ReadOnlySequence<byte> line))
            {
                ProcessLine(line, ++lineNumber);
            }

            reader.AdvanceTo(buffer.Start, buffer.End);
            if (result.IsCompleted)
            {
                break;
            }
        }
        await reader.CompleteAsync();
    }

    private static bool TryReadLine(ref ReadOnlySequence<byte> buffer,
        out ReadOnlySequence<byte> line)
    {
        var position = buffer.PositionOf((byte)'\n');
        if (!position.HasValue) {
            line = default;
            return false; 
        }
        line = buffer.Slice(0, position.Value);
        buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
        return true;
    }

    private static void ProcessLine(ReadOnlySequence<byte> line, int lineNumber)
    {
        var lineStr = Encoding.ASCII.GetString(line);
        Console.WriteLine("Line #{0}: {1}", lineNumber, lineStr);
    }

    private static async Task WriteToPipe(string input, PipeWriter writer)
    {
        var bytes = Encoding.ASCII.GetBytes(input);
        var buffer = writer.GetMemory(bytes.Length);
        bytes.CopyTo(buffer);
        writer.Advance(bytes.Length);
        await writer.FlushAsync();
        await writer.CompleteAsync();
    }
    public static void Main(string[] args)
    {
        string input = @"
This is a multiline string
This is the third line
This is the fourth line

This is the last line
";
        var task = PipeAndPipelines.ProcessData(input);
        task.Wait();
    }
}