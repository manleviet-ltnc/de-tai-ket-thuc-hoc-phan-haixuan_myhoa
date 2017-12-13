using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication3
{
    class Publish
    {
        private int[] Problem;
        private int[] Result;
        private bool PublishResult;
        private string Path;
        public Publish(bool publishresult, int[] problem, int[] result, string pathfile)
        {
            PublishResult = publishresult;
            Problem = problem;
            Result = result;
            Path = pathfile;
        }
        private string color(int i)
        {
            int u = ((i-1)%9 ) / 3;
            int v = ((i - 1) / 9) / 3;
            return (u + v) % 2 == 0 ? "bgcolor=\"#CCFFCC\"" : "";
        }
        public string WriteToFileHTML()
        {
            try
            {
                FileStream file;
                try
                {
                    file = new FileStream(Path, FileMode.CreateNew);
                }
                catch
                {
                    file = new FileStream(Path, FileMode.OpenOrCreate );
                }
                    StreamWriter str = new StreamWriter(file);
                str.WriteLine("<html>");
                str.WriteLine("<head>");
                str.WriteLine("<meta http-equiv=\"Content-Language\" content=\"en-us\">");
                str.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">");
                str.WriteLine("<title>Sudoku</title>");
                str.WriteLine("</head>");
                str.WriteLine("<body>");
                
                str.WriteLine("<div align=\"center\"><b>");
                str.WriteLine("<font size=\"7\">&#272;&#7873;</font><font size=\"7\"> Sudoku</font><hr>");
                str.WriteLine("	<table border=\"1\" width=\"38%\" id=\"table1\" height=\"350\">");
                for (int i = 0; i < 81; i++)
                {
                    if (i % 9 == 0) str.WriteLine("<tr>");
                    str.WriteLine("<td align=\"center\" " + color(i + 1) + "\">");
                    str.WriteLine("<font size=\"6\">");
                    if(Problem [i+1]==0)
                        str.WriteLine("&nbsp;");
                    else
                        str.WriteLine(Problem[i+1].ToString());
                    str.WriteLine("</font>");
                    str.WriteLine("</td>");
                    if (i % 9 == 8) str.WriteLine("</tr>");
                }
                str.WriteLine("</table>");


                if (PublishResult)
                {
                    str.WriteLine("<font size=\"7\">&#272;&#225;p &#225;n</font><hr>");
                    str.WriteLine("	<table border=\"1\" width=\"38%\" id=\"table1\" height=\"350\" >");
                    for (int i = 0; i < 81; i++)
                    {
                        if (i % 9 == 0) str.WriteLine("<tr>");
                        str.WriteLine("<td align=\"center\" " + color(i + 1) + "\">");
                        str.WriteLine("<font size=\"6\">");
                        if (Problem[i + 1] != 0)
                            str.WriteLine("<font color=\"red\">" + Result[i + 1].ToString()+"</font>");
                        else
                            str.WriteLine("<font color=\"black\">" + Result[i + 1].ToString() + "</font>");
                        str.WriteLine("</font>");
                        str.WriteLine("</td>");
                        if (i % 9 == 8) str.WriteLine("</tr>");
                    }
                    str.WriteLine("</table>");

                }
                str.WriteLine("</b></div>");
                str.WriteLine("</body>");
                str.WriteLine("</html>");
                str.Close();
                file.Close();

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
        public string  WriteToFileRTF()
        {
            try
            {
                FileStream file;
                try
                {
                    file = new FileStream(Path, FileMode.CreateNew);
                }
                catch
                {
                    file = new FileStream(Path, FileMode.OpenOrCreate);
                }
                StreamWriter str = new StreamWriter(file);

                str.WriteLine("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033\\deflangfe1033{\\fonttbl{\\f0\\froman\\fprq2\\fcharset0 Times New Roman;}}");
                str.WriteLine("{\\colortbl ;\\red255\\green0\\blue0;\\red0\\green255\\blue255;}");
                str.WriteLine("{\\*\\generator Msftedit 5.41.15.1515;}\\viewkind4\\uc1\\pard\\qc\\cf1\\f0\\fs56\\u272?\\u7873? Sudoku\\par");
                str.WriteLine("\\cf0\\fs24\\par");
                str.WriteLine("\\trowd\\trgaph108\\trleft-108\\trqc\\trrh437\\trbrdrl\\brdrs\\brdrw10 \\trbrdrt\\brdrs\\brdrw10 \\trbrdrr\\brdrs\\brdrw10 \\trbrdrb\\brdrs\\brdrw10 \\trpaddl108\\trpaddr108\\trpaddfl3\\trpaddfr3");
                for (int i = 1; i <= 9; i++)
                {
                    if (i >= 4 && i <= 6) str.Write("\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx505\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1118\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1731\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2344\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2957\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx3570\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4183\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4796\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx5409\\pard\\intbl\\nowidctlpar\\qc");
                    else str.Write("\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx505\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1118\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1731\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2344\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2957\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx3570\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4183\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4796\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx5409\\pard\\intbl\\nowidctlpar\\qc");
                    if (i == 1) str.Write("\\fs32");
                    for (int j = 1; j <= 9; j++)
                    {
                        str.Write(" ");
                    if((Problem[(i - 1) * 9 + j])==0) str.Write(" ");
                    else str.Write("\\cf1 " + Problem[(i - 1) * 9 + j].ToString());//
                    str.Write(" \\cell");
                    }
                    if (i >= 4 && i <= 6) str.WriteLine("\\row\\trowd\\trgaph108\\trleft-108\\trqc\\trrh437\\trbrdrl\\brdrs\\brdrw10 \\trbrdrt\\brdrs\\brdrw10 \\trbrdrr\\brdrs\\brdrw10 \\trbrdrb\\brdrs\\brdrw10 \\trpaddl108\\trpaddr108\\trpaddfl3\\trpaddfr3");
                    else if (i != 9) str.WriteLine("\\row\\trowd\\trgaph108\\trleft-108\\trqc\\trrh437\\trbrdrl\\brdrs\\brdrw10 \\trbrdrt\\brdrs\\brdrw10 \\trbrdrr\\brdrs\\brdrw10 \\trbrdrb\\brdrs\\brdrw10 \\trpaddl108\\trpaddr108\\trpaddfl3\\trpaddfr3");
                    else str.WriteLine("\\row\\pard\\nowidctlpar\\qc\\cf1\\fs56\\par");
                }
                if (PublishResult)
                {
                    str.WriteLine("\\u272?\\'e1p \\'e1n\\par");
                    str.WriteLine("\\par");
                    str.WriteLine("\\trowd\\trgaph108\\trleft-108\\trqc\\trrh437\\trbrdrl\\brdrs\\brdrw10 \\trbrdrt\\brdrs\\brdrw10 \\trbrdrr\\brdrs\\brdrw10 \\trbrdrb\\brdrs\\brdrw10 \\trpaddl108\\trpaddr108\\trpaddfl3\\trpaddfr3");

                    for (int i = 1; i <= 9; i++)
                    {
                        if (i >= 4 && i <= 6) str.Write("\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx505\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1118\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1731\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2344\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2957\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx3570\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4183\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4796\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx5409\\pard\\intbl\\nowidctlpar\\qc");
                        else str.Write("\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx505\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1118\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx1731\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2344\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx2957\\clvertalc\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx3570\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4183\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx4796\\clvertalc\\clcbpat2\\clbrdrl\\brdrw10\\brdrs\\clbrdrt\\brdrw10\\brdrs\\clbrdrr\\brdrw10\\brdrs\\clbrdrb\\brdrw10\\brdrs \\cellx5409\\pard\\intbl\\nowidctlpar\\qc");
                        if (i == 1) str.Write("\\fs32");
                        for (int j = 1; j <= 9; j++)
                        {
                            str.Write(" ");
                            if ((Problem[(i - 1) * 9 + j]) == 0) str.Write("\\cf0 ");
                            else str.Write("\\cf1 ");
                            str.Write(Result[(i - 1) * 9 + j].ToString() + " \\cell");
                        }
                        if (i >= 4 && i <= 6) str.WriteLine("\\row\\trowd\\trgaph108\\trleft-108\\trqc\\trrh437\\trbrdrl\\brdrs\\brdrw10 \\trbrdrt\\brdrs\\brdrw10 \\trbrdrr\\brdrs\\brdrw10 \\trbrdrb\\brdrs\\brdrw10 \\trpaddl108\\trpaddr108\\trpaddfl3\\trpaddfr3");
                        else if (i != 9) str.WriteLine("\\row\\trowd\\trgaph108\\trleft-108\\trqc\\trrh437\\trbrdrl\\brdrs\\brdrw10 \\trbrdrt\\brdrs\\brdrw10 \\trbrdrr\\brdrs\\brdrw10 \\trbrdrb\\brdrs\\brdrw10 \\trpaddl108\\trpaddr108\\trpaddfl3\\trpaddfr3");
                        else str.WriteLine("\\row\\pard\\nowidctlpar\\qc\\cf1\\fs56\\par");
                    }
                }
                str.Write("}\n\0");
                str.Close();
                file.Close();

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
    }
}
