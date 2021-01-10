using NUnit.Framework;
using IIG.BinaryFlag;
using IIG.FileWorker;

namespace KPI.IntegrationTesting
{
    
    class BinaryFlagWorksWithFileWriterTesting
    {
        private const string path = "C:\\Users\\masha\\source\\repos\\KPI\\KPI\\IntegrationTesting\\textFilesSrc\\";

        [Test]
        public void WriteFile()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(3, true);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(5, true);
            flag2.ResetFlag(0);
            string text = flag1.ToString() + " " + flag1.GetFlag() + "\r\n" + flag2.ToString() + " " + flag2.GetFlag();
            Assert.IsTrue(BaseFileWorker.Write(text, path + "runtimeRecordedFile.txt"));
        }

        [Test]
        public void ReadLines_FromRuntimeRecordedFile()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(3, true);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(5, true);
            flag2.ResetFlag(0);
            string[] lines = BaseFileWorker.ReadLines(path + "runtimeRecordedFile.txt");
            Assert.AreEqual(lines[0], flag1.ToString() + " " + flag1.GetFlag());
            Assert.AreEqual(lines[1], flag2.ToString() + " " + flag2.GetFlag());
        }

        [Test]
        public void ReadLines_FromPreRecordedFile()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(4, false);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(4, true);
            flag1.SetFlag(2);
            string[] lines = BaseFileWorker.ReadLines(path + "preRecordedFile.txt");
            Assert.AreEqual(lines[0], flag1.ToString() + " " + flag1.GetFlag());
            Assert.AreEqual(lines[1], flag2.ToString() + " " + flag2.GetFlag());
        }

        [Test]
        public void ReadLines_FromFile_LinesAreNotEqual()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(4, false);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(4, true);
            flag1.SetFlag(2);
            string[] lines = BaseFileWorker.ReadLines(path + "runtimeRecordedFile.txt");
            Assert.AreNotEqual(lines[0], flag1.ToString() + " " + flag1.GetFlag());
            Assert.AreNotEqual(lines[1], flag2.ToString() + " " + flag2.GetFlag());
        }

        [Test]
        public void ReadAll_FromRuntimeRecordedFile()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(3, true);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(5, true);
            flag2.ResetFlag(0);
            string text = flag1.ToString() + " " + flag1.GetFlag() + "\r\n" + flag2.ToString() + " " + flag2.GetFlag();
            string readedText = BaseFileWorker.ReadAll(path + "runtimeRecordedFile.txt");
            Assert.AreEqual(text, readedText);
        }

        [Test]
        public void ReadAll_FromFromPreRecordedFile()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(4, false);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(4, true);
            flag1.SetFlag(2);
            string text = flag1.ToString() + " " + flag1.GetFlag() + "\r\n" + flag2.ToString() + " " + flag2.GetFlag();
            string readedText = BaseFileWorker.ReadAll(path + "preRecordedFile.txt");
            Assert.AreEqual(text, readedText);
        }

        [Test]
        public void ReadAll_FromFile_LinesAreNotEqual()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(3, true);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(5, true);
            flag2.ResetFlag(0);
            string text = flag1.ToString() + " " + flag1.GetFlag() + "\r\n" + flag2.ToString() + " " + flag2.GetFlag();
            string readedText = BaseFileWorker.ReadAll(path + "preRecordedFile.txt");
            Assert.AreNotEqual(text, readedText);
        }
    }
}
