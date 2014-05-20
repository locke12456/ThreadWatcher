﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace libUtilities
{

    public class Search
    {
        private List<string> _paths;
        private string _file_type = "normal";
        ///**
        // * usage :
        // *      paths = "-IC:/VC/Win32;D:/Qt/4.8";
        // * */
        //public Search( string paths ) 
        //{

        //}
        /**
         * usage :
         *      paths = ["C:/VC/Win32","D:/Qt/4.8"]
         * */
        public Search(List<string> paths)
        {
            _paths = paths;
        }
        /**
         * usage :
         *      paths = ["C:/VC/Win32","D:/Qt/4.8"]
         *      
         * */
        public Search(List<string> paths, string file_type)
        {
            _paths = paths;
            _file_type = file_type;
        }

        public FileInfo GetFile(string filename, int count = 1)
        {
            return _findInPaths(filename, count);
        }

        private FileInfo _findInPaths(string file, int count)
        {

            List<FileInfo> files = new List<FileInfo>();

            string[] variable = (file.IndexOf("%") == -1) ? null : (Parser.ParseEnvironmentVariable(file));

            DirectoryInfo directory = variable != null ? new DirectoryInfo(Environment.GetEnvironmentVariable(variable[0])) : null;

            if (directory != null)
            {
                file = variable[1];
                FileInfo test_file = new FileInfo(directory.FullName + file);
                if (File.Exists(test_file.FullName))
                    return test_file;
            }

            foreach (string path in _paths)
            {
                _findFileInDirectory_recursive(path, file, files);
                _findFileInDirectory(path, file, files);
            }

            //if (files.Count > count) throw (_fileFound(_file_type + files));
            if (files.Count == 0) throw (new Exception(_file_type + file));
            return files[0];
        }
        //private TasException _fileFound(string mode, List<FileInfo> files)
        //{
        //    Func<List<FileInfo>, TasException> fail;

        //    Dictionary<string, Func<List<FileInfo>, TasException>> modes = new Dictionary<string, Func<List<FileInfo>, TasException>>() 
        //    {
        //        {"normal" , ( List<FileInfo> f)=> { return new TasIncludeFileNamingError(f);   }},
        //        {"project" , ( List<FileInfo> f)=> { return new TasProjectFileConditionError(f); }}
        //    };
        //    Debug.Assert(modes.TryGetValue(mode, out fail));

        //    return fail(files);
        //}

        //private TasException _fileNotFound(string mode, string file)
        //{
        //    Func<string, TasException> fail;

        //    Dictionary<string, Func<string, TasException>> modes = new Dictionary<string, Func<string, TasException>>() 
        //    {
        //        {"normal" , ( string f)=> { TasIncludeFileNotFound failed = new TasIncludeFileNotFound();
        //                                    failed.Files = new List<FileInfo>(){ new FileInfo(file) };return failed;    }},
        //        {"project" , ( string f)=> { return new TasProjectFileNotFound(); }}
        //    };
        //    Debug.Assert(modes.TryGetValue(mode, out fail));

        //    return fail(file);
        //}
        private void _checkFileInList(string check, List<FileInfo> list)
        {
            FileInfo file = new FileInfo(check);
            if (list.Count == 0) { list.Add(file); return; }
            foreach (FileInfo info in list)
            {
                if (info.FullName == file.FullName)
                {
                    return;
                }
            }
            list.Add(file);
        }
        private void _findFileInDirectory_recursive(string path, string file, List<FileInfo> list)
        {
            string[] fileinfo = _findInDir(path, file);
            if (fileinfo != null)
            {
                foreach (string getfile in fileinfo)
                {
                    _checkFileInList(getfile, list);
                }
            }
        }

        private void _findFileInDirectory(string path, string file, List<FileInfo> list)
        {
            string[] fileinfo = _findInPath(path, file);
            if (fileinfo != null)
            {
                foreach (string getfile in fileinfo)
                {
                    _checkFileInList(getfile, list);
                }
            }
        }
        private string[] _findInDir(string path, string file, List<string> filelist = null)
        {
            List<string> finfo = filelist != null ? filelist : new List<string>();
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            foreach (string dir in Directory.GetDirectories(dirinfo.FullName))
            {
                string[] filename = _findInPath(dir, file);
                if (filename != null) finfo.AddRange(filename);
                _findInDir(dir, file, finfo);
            }
            //if (finfo.Count > 1) throw (new System.Exception(Exception.PATH_ERROR));
            return finfo.Count > 0 ? finfo.ToArray() : null;
        }
        private string[] _findInPath(string path, string file)
        {
            try
            {
                FileInfo info = new FileInfo(file);
                file = info.Name;
            }
            catch (Exception notfile)
            {

            }
            string[] finfo = Directory.GetFiles(path, file);
            //if (finfo.Length > 1) throw (new System.Exception(Exception.PATH_ERROR));
            return finfo.Length > 0 ? finfo : null;
        }


    }
}