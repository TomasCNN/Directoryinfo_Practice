class DirectoryinfoPractice
{
    static void Main(string[] args)
    {
        #region 初始化根目录对象
        // 初始化根文件夹路径
        string folderPath = @".\\DirectoryinfoPractice";

        // 初始化一个DirectoryInfo对象
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

        // 创建一个DirectoryInfo对象（可有可没有）
        directoryInfo.Create();
        #endregion

        #region 初始化根目录副本对象
        // 初始化一个根文件夹副本路径
        string folderCopyPath = @".\\CDirectoryinfoPractice";

        // 初始化一个CDirectoryInfo对象
        DirectoryInfo cdirectoryInfo = new DirectoryInfo(folderCopyPath);

        // 创建一个DirectoryInfo对象（可有可没有）
        cdirectoryInfo.Create();
        #endregion

        #region 初始化根目录副本Delate对象
        // 初始化一个根文件夹副本路径
        string folderCopyPathD = @".\\CDirectoryinfoPracticeD";

        // 初始化一个CDirectoryInfo对象
        DirectoryInfo DcdirectoryInfo = new DirectoryInfo(folderCopyPathD);

        // 创建一个DirectoryInfo对象（可有可没有）
        DcdirectoryInfo.Create();
        #endregion

        #region 确定根文件夹是否创建成功
        // 确定根文件夹是否创建成功
        if (Directory.Exists(folderPath))
        {
            Console.WriteLine(directoryInfo.FullName + "创建成功！");
        }
        else
        {
            Console.WriteLine("根文件夹创建失败！");
        }
        #endregion

        #region 确定根文件夹副本是否创建成功
        // 确定根文件夹副本是否创建成功
        if (Directory.Exists(folderCopyPath))
        {
            Console.WriteLine(cdirectoryInfo.FullName + "创建成功！");
        }
        else
        {
            Console.WriteLine("根文件夹副本创建失败！");
        }
        #endregion

        #region 确定根文件夹副本Delate是否创建成功
        // 确定根文件夹副本Delate是否创建成功
        if (Directory.Exists(folderCopyPathD))
        {
            Console.WriteLine(DcdirectoryInfo.FullName + "创建成功！");
        }
        else
        {
            Console.WriteLine("根文件夹副本创建失败！");
        }
        #endregion


        // 创建一个D:\\DirectoryinfoPractice根目录文件夹下的子目录文件夹code-1
        directoryInfo.CreateSubdirectory("code-1");



        // 创建一个D:\\DirectoryinfoPractice根目录文件夹下的子目录文件夹code-2
        directoryInfo.CreateSubdirectory("code-2");

        // 添加一个名为“test1.txt”的新文件
        StreamWriter test1 = File.AppendText(folderPath + "\\test1.txt");

        // 添加一个名为“test2.txt”的新文件
        StreamWriter test2 = File.AppendText(folderPath + "\\test2.txt");

        StreamWriter test3 = File.AppendText(folderPath + "\\code-1" + "\\test1.txt");

        // 关闭名为“test1.txt”的文件，必须关闭，否则会一直占用
        test1.Close();

        // 关闭名为“test1.txt”的文件
        test2.Close();

        test3.Close();

        // 初始化一个子文件夹路径
        string newSubfolderPath = Path.Combine(folderPath, "newSubFolder");

        // 初始化一个子文件夹对象
        DirectoryInfo newSubDirectory = directoryInfo.CreateSubdirectory("newSubfolder");

        // 将根文件夹拷贝到根文件夹副本中
        DirecoryCopyTo(folderPath, folderCopyPath);


        DirecoryCopyTo(folderCopyPath, folderCopyPathD);

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

    /// <summary>
    /// 自定义一个文件夹拷贝专用方法
    /// 此方法原理：在目标文件夹先创建一个与源文件夹相同名称的文件夹，然后获取源文件夹下所有的文件夹和文件
    /// 对于文件，直接移动到目标文件夹下对应的源文件夹相同名称的文件夹内
    /// 对于文件夹，首先在目标文件目标里创建一个相同名称的文件夹，然后扫描源文件夹内的内容
    /// 方法需输入：源文件夹路径sourcePth和拷贝后文件夹路径copyPath
    /// </summary>
    public static void DirecoryCopyTo(string sourcePth, string copyPath)
    {

        try
        {
            // 判断源文件夹路径下是否有源文件夹
            if (!Directory.Exists(sourcePth))
            {
                // 如果源文件夹夹不存在，就新建一个源文件夹（也可根据需要执行其他操作）
                Directory.CreateDirectory(sourcePth);
            }

            // 获取源文件夹中各文件的名称并存储一个字符串数组中
            string[] filesNames = Directory.GetFiles(sourcePth);

            // 循环遍历源文件夹中文件名称
            foreach (var fitem in filesNames)
            {
                // 初始化临时变量用当前源文件夹中文件名称 + 待拷贝文件夹路径生成待拷贝文件夹下的对应文件
                string tempPath = copyPath + "\\" + Path.GetFileName(fitem);
                if (File.Exists(tempPath))
                {
                    continue;
                }
                File.Copy(fitem, tempPath, true);
            }

            // 获取源文件夹中各文件夹的名称并存储一个字符串数组中
            string[] directoryName = Directory.GetDirectories(sourcePth);
            foreach (var ditem in directoryName)
            {
                DirecoryCopyTo(ditem, copyPath + "\\" + Path.GetFileName(ditem));
                Directory.CreateDirectory($"{copyPath + "\\" + Path.GetFileName(ditem)}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("程序异常！");
        }
    }
}