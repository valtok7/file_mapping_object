cmake_minimum_required(VERSION 3.0)
project(FileMappingObjectConsumer)

set(CMAKE_CXX_STANDARD 20)

# cppファイルをビルド対象に加える
file(GLOB_RECURSE SOURCES "src/*.cpp")

# 実行ファイル作る
add_executable(FileMappingObjectConsumer ${SOURCES})
