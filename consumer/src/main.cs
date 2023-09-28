using System;
using System.IO.MemoryMappedFiles;
using System.Text;

class Program
{
    static void Main()
    {
        const string mapName = "SharedMemory";
        const int dataSize = 1024;

        // ファイルマッピングオブジェクトをオープン
        using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(mapName))
        {
            // メモリマップされたファイルにアクセス
            using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
            {
                // データを読み取る (コンシューマー)
                byte[] buffer = new byte[dataSize];
                accessor.ReadArray(0, buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer);

                Console.WriteLine("プロセス2からのメッセージ: " + message);
            }
        }
    }
}
