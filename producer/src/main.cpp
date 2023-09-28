#include <iostream>
#include <windows.h>

int main() {
    const char* mapName = "SharedMemory";
    const int dataSize = 1024;

    // ファイルマッピングオブジェクトを作成またはオープン
    HANDLE hMapFile = CreateFileMapping(
        INVALID_HANDLE_VALUE, // ファイルハンドルを指定しない
        NULL,                 // セキュリティ属性 (デフォルト)
        PAGE_READWRITE,       // 読み書きアクセス
        0,                    // ハイオーダー ファイル サイズ
        dataSize,             // 低オーダー ファイル サイズ
        mapName               // マップオブジェクト名
    );

    if (hMapFile == NULL) {
        std::cerr << "ファイルマッピングオブジェクトを作成できませんでした。エラーコード: " << GetLastError() << std::endl;
        return 1;
    }

    // マップされたファイルのビューを取得
    LPVOID mappedData = MapViewOfFile(
        hMapFile,
        FILE_MAP_ALL_ACCESS,
        0,
        0,
        dataSize
    );

    if (mappedData == NULL) {
        std::cerr << "ファイルをマップできませんでした。エラーコード: " << GetLastError() << std::endl;
        CloseHandle(hMapFile);
        return 1;
    }

    // データを書き込む (プロデューサー)
    const char* message = "Hello from Process 1!";
    strncpy(static_cast<char*>(mappedData), message, strlen(message));

    // マップをアンマップ
    UnmapViewOfFile(mappedData);

    // ファイルマッピングオブジェクトをクローズ
    CloseHandle(hMapFile);

    std::cout << "プロセス1がデータを書き込みました。" << std::endl;

    return 0;
}
