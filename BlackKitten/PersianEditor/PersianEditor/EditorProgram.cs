/* 
 * In the name of ALLAH, the compassionate and the merciful
 * Project              : PersianEditor
 * Programmer and Owner : Pooya Eimandar
 * Company              : Persian GameDevelopers
 * started              : July 2011
 */

using System;
using System.Windows.Forms.Integration;

namespace PersianEditor
{
#if WINDOWS
    static class EditorProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            string HResult = null;
            
            //set Win Theme
            EditorShared.SetTheme(EditorThemes.Black);
            Persian.GetSharedParams(true, args);

            #region Start Logger

            string HR = Logger.Start(Persian.CurrentDir, true, Persian.EVersion);
            if (HR != null) Logger.WriteWarning(HR);
            if (HResult != null)
            {
                Logger.WriteError(HResult);//This Error comes from Content Dir
                ExitMain();
            }

            #endregion

            #region Check 4 content and it's base directories

            HResult = EditorShared.CreateEngineContentDirs(false);
            if (HResult != null)
            {
                Logger.WriteError(String.Format("Could not create content directories of engine because : {0}", HResult));
                ExitMain();
            }
            Logger.WriteLine("Content directories of engine created");

            HResult = EditorShared.CreateEditorContentDirs(false);
            if (HResult != null)
            {
                Logger.WriteError(String.Format("Could not create content directories of editor because : {0}", HResult));
                ExitMain();
            }
            Logger.WriteLine("Content directories of editor created");

            #endregion

            #region Run Editor

            Logger.WriteLine(String.Format("Starting Editor at {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));

            using (var Shell = new Windows.ShellWin())
            {
                var xna = new XNA.ShellXna(Shell.Render_Layer.Handle);
                //Enable Keyboard INT
                ElementHost.EnableModelessKeyboardInterop(Shell);
                //WindowsFormsHost.EnableWindowsFormsInterop();
                Shell.Show();
                xna.Run();
            }

            #endregion

            ExitMain();
        }

        #region Exiting

        static void ExitMain()
        {
            Logger.WriteNotice(string.Format("{0} Resources disposed", SystemMemory.DisposedResource));
            Logger.WriteNotice(string.Format("{0} pointers relased", SystemMemory.FreePointers));
            Logger.WriteLine(String.Format("Exiting from Editor at {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
            Logger.ChangeColor(ConsoleColor.Cyan);
            Logger.WriteLine("Press any key...");
            Console.ReadKey();
            Logger.Dispose();
            Environment.Exit(0);
        }

        #endregion
    }
#endif
}

