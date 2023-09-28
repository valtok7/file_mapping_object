#include <iostream>
#include <windows.h>

int main() {
    const char* mapName = "SharedMemory";
    const int dataSize = 1024;

    // ファイルマッピングオブジェクトをオープン
    HANDLE hMapFile = OpenFileMapping(
        FILE_MAP_ALL_ACCESS,
        FALSE,     // インヘリタブルなセキュリティ記述子を使用しない
        mapName    // マップオブジェクト名
    );

    if (hMapFile == NULL) {
        std::cerr << "ファイルマッピングオブジェクトをオープンできませんでした。エラーコード: " << GetLastError() << std::endl;
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

    // データを読み取る (コンシューマー)
    std::cout << "プロセス2からのメッセージ: " << static_cast<char*>(mappedData) << std::endl;

    // マップをアンマップ
    UnmapViewOfFile(mappedData);

    // ファイルマッピングオブジェクトをクローズ
    CloseHandle(hMapFile);

    return 0;
}
