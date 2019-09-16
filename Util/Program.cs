using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Util
{
    class Program
    {
        private int iErrorCode = 0;
        private int iFileLineCount = 0;
        public static int iError_EmptyParam     =   100;
        public static int iError_InvalidParam   =   101;
        public static int iError_UnSupportParam =   102;
        
        public static int iError_FileNotExist   =   202;
        public static int iError_Exception      =   901;

        public static int iFlags_PhraseToUpper  =   500;
        public static int iFlags_PhraseToLower  =   500;
        public static int iFlags_RemoveNumber   =   510;
        public static int iFlags_RemoveEnglish  =   511;
        public static int iFlags_RemoveChinese  =   512;
        public static int iFlags_RemoveTag      =   513; 
        public static int iFlags_RemoveSpace    =   514;
        public static int iFlags_RemoveEOL      =   515;
        public static int iFlags_RemoveTab      =   516;

        static void Main(string[] args)
        {

        }

        public int Util_GetErrorCode() {
            return iErrorCode;
        }

        public string Util_RemoveByList(string sOrgString, string[] sTargitWords) {
            string sOutString = "";
            if (!String.IsNullOrEmpty(sOrgString) && !String.IsNullOrEmpty(sTargitWords[0]))
            {
                try
                {
                    for (int i = 0; i < sTargitWords.Length; i++)
                    {
                        sOutString = Regex.Replace(sOrgString, sTargitWords[i], "");
                    }
                }
                catch (Exception ex) {
                    iErrorCode = iError_Exception;
                }
            }
            else {
                iErrorCode = iError_EmptyParam;
            }
            return sOutString;
        }

        public string Util_Remove(string sOrgString, int iFlags)
        {
            string sOutString = "";
            if (!String.IsNullOrEmpty(sOrgString))
            {
                switch (iFlags) {
                    case 510: //iFlags_RemoveNumber 去除數字
                        sOutString = Regex.Replace(sOrgString, @"[0-9]", String.Empty);
                        break;
                    case 511: //iFlags_RemoveEnglish 去除英文
                        sOutString = Regex.Replace(sOrgString, @"[a-z]", String.Empty);
                        break;
                    case 512: //iFlags_RemoveChinese 去除中文 #edit
                        iErrorCode = iError_UnSupportParam;
                        break;
                    case 513: //iFlags_RemoveTag 去除Tag
                        sOutString = Regex.Replace(sOrgString, @"<[^>]*>", String.Empty);
                        break;
                    case 514: //iFlags_RemoveSpace 去除空白
                        sOutString = Regex.Replace(sOrgString, @" ", String.Empty);
                        sOutString = Regex.Replace(sOrgString, @"\r", String.Empty);
                        break;
                    case 515: //iFlags_RemoveEOL 去除換行
                        sOutString = Regex.Replace(sOrgString, @"\n", String.Empty);
                        break;
                    case 516: //iFlags_RemoveTab 去除Tab
                        sOutString = Regex.Replace(sOrgString, @"\t", String.Empty);
                        break;
                    default:
                        iErrorCode = iError_InvalidParam;
                        break; 
                }
            }
            else
            {
                iErrorCode = iError_EmptyParam;
            }
            return sOutString;
        }

        public string Util_ConvertPhrase(string sOrgString, int iFlags) {
            string sOutString = "";
            if (iFlags == iFlags_PhraseToUpper)
            {
                sOrgString = sOrgString.ToUpper();
            }
            else if (iFlags == iFlags_PhraseToLower)
            {
                sOrgString = sOrgString.ToLower();
            }
            else {
                iErrorCode = iError_InvalidParam;
            }

            return sOutString;
        }

        public string Util_ReadAllFile(string sFilePath){
            string sOutString = "";
            bool bFileExist = File.Exists(sFilePath);
            if (bFileExist)
            {
                StreamReader str = new StreamReader(sFilePath);
                sOutString = str.ReadToEnd();
                str.Close();
            }
            else {
                iErrorCode = iError_FileNotExist;
            }
            return sOutString;
        }

        public string[] Util_ReadFilebyLine(string sFilePath)
        {
            string[] sOutString;
            
            bool bFileExist = File.Exists(sFilePath);
            if (bFileExist)
            {
                StreamReader str = new StreamReader(sFilePath);
                while (!str.EndOfStream)
                { 
                    ++iFileLineCount;  
                }
                str.Dispose();
                str.Close();

                if (iFileLineCount > 0)
                {
                    str = new StreamReader(sFilePath);
                    sOutString = new string[iFileLineCount];
                    int count = 0;
                    while (!str.EndOfStream)
                    {               
                        sOutString[count] = str.ReadLine();
                        count++;
                    }
                    str.Dispose();
                    str.Close();
                }
                else {
                    sOutString = new string[1];
                }                
            }
            else
            {
                sOutString = new string[1];
                iErrorCode = iError_FileNotExist;
            }
            
            return sOutString;
        }

        public int Util_GetFileLineCount() {
            return iFileLineCount;
        }
    }
}
