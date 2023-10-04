using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileMappingObject
{

    public partial class Form1 : Form
    {
        private MemoryMappedFile mmf;
        public Form1()
        {
            InitializeComponent();

            const string mapName = "SharedMemory";
            const int dataSize = 1024;

            // ファイルマッピングオブジェクトを作成
            mmf = MemoryMappedFile.CreateNew(mapName, dataSize);
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
}
