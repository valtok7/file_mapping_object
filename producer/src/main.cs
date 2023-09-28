using System;
using System.IO.MemoryMappedFiles;
using System.Text;

class Program
{
    static void Main()
    {
        const string mapName = "SharedMemory";
        const int dataSize = 1024;

        // ファイルマッピングオブジェクトを作成
        using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew(mapName, dataSize))
        {
            // メモリマップされたファイルにアクセス
            using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
            {
                // データを書き込む (プロデューサー)
                string message = "Hello from Process 1!";
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                accessor.WriteArray(0, buffer, 0, buffer.Length);

                Console.WriteLine("プロセス1がデータを書き込みました。");
            }
        }
    }
}
