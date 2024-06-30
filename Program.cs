using System;
using System.IO;
class  DirectoryinfoPractice
{
    static void Main(string[] args)
    {
        // 初始化根文件夹路径
        string folderPath = @".\\DirectoryinfoPractice";

        // 初始化一个DirectoryInfo对象
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

        // 创建一个DirectoryInfo对象（可有可没有）
        directoryInfo.Create();

        // 确定文件夹是否创建成功
        Console.WriteLine(directoryInfo.FullName + "创建成功！");

        // 创建一个D:\\DirectoryinfoPractice根目录文件夹下的子目录文件夹code-1
        directoryInfo.CreateSubdirectory("code-1");
        
        // 创建一个D:\\DirectoryinfoPractice根目录文件夹下的子目录文件夹code-2
        directoryInfo.CreateSubdirectory("code-2");

        // 添加一个名为“test1.txt”的新文件
        StreamWriter test1 = File.AppendText(folderPath + "\\test1.txt");

        // 添加一个名为“test2.txt”的新文件
        StreamWriter test2 = File.AppendText(folderPath + "\\test2.txt");

        // 关闭名为“test1.txt”的文件，必须关闭，否则会一直占用
        test1.Close();

        // 关闭名为“test1.txt”的文件
        test2.Close();

        // 判断D:\\DirectoryinfoPractice根目录文件夹是否创建成功
        if (directoryInfo.Exists)
        {
            // 输出根目录文件夹名称
            Console.WriteLine($"Folder name: {directoryInfo.Name}");

            // 输出根目录文件夹的创建时间
            Console.WriteLine($"Creation time: {directoryInfo.CreationTime}");

            // 输出根目录文件夹最后一次的被访问时间
            Console.WriteLine($"Last access time: {directoryInfo.LastAccessTime}");

            // 输出根目录文件夹最后一次写入的时间
            Console.WriteLine($"Last write time: {directoryInfo.LastWriteTime}");
            
        }

        // 获取根文件夹中子文件夹列表
        IEnumerable<DirectoryInfo> dir = directoryInfo.EnumerateDirectories();

        // 循环输出根文件夹中子文件夹名称
        foreach (var v in dir)
        {
            Console.WriteLine(v.Name);
        }

        // 获取根文件夹中子文件列表
        FileInfo[] files = directoryInfo.GetFiles();

        // 循环输出根文件夹中子文件名称
        foreach (var f in files)
        {
            Console.WriteLine(f.Name);
        }

        // 初始化一个子文件夹路径
        string newSubfolderPath = Path.Combine(folderPath,"newSubFolder");

        // 初始化一个子文件夹对象
        DirectoryInfo newSubDirectory = directoryInfo.CreateSubdirectory("newSubfolder");

        // 移动文件夹到新路径
        // 初始化待移动文件夹路径
        string movedFolderPath = @".\\moveSubfolder";

        // 初始化待移动文件夹对象
        DirectoryInfo moveSubfolder = new DirectoryInfo(movedFolderPath);

        // 判断待移动文件夹是否存在，若已存在，需删除(目标文件名不能存在！)
        if (moveSubfolder.Exists)
        {
            Directory.Delete(movedFolderPath, true);
        }

        // 将根文件夹中的内容移动到待移动文件夹中
        directoryInfo.MoveTo(movedFolderPath);







    }
}