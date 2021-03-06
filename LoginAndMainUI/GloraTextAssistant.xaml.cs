﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech;
using System.Diagnostics;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Media.Animation;
using System.Reflection;
using System.Net;
using Microsoft.Win32;
using MaterialDesignColors.Recommended;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for GloraTextAssistant.xaml
    /// </summary>
    public partial class GloraTextAssistant : UserControl
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        List<string> browsers = new List<string>();
        Choices choice = new Choices();
        int indexCommands = 0;
        string[] commands = new string[30];
        string search = "";
        string path = "";
        string allTxt = "";
        string helpSearch = "";
        bool speak = false;
        bool cantOpen = false;
        bool problems = false;
        bool problemsHelp = false;
        bool shutProgramDown = false;
        bool howToProgram = false;
        bool beginerProgrammer = false;
        bool startPython = false;
        bool startC = false;
        bool cRedirect = false;
        bool pythonRedirect = false;
        bool openBrowsers = false;
        bool openFile = false;
        string name;
        bool anim = true;
        string[] motivate = { "The harder you work for something, the greater you’ll feel when you achieve it.", "Don’t stop when you’re tired. Stop when you’re done.", "Do something today that your future self will thank you for.", " Sometimes we’re tested not to show our weaknesses, but to discover our strengths.", "The key to success is to focus on goals, not obstacles.", "The pessimist sees difficulty in every opportunity. The optimist sees opportunity in every difficulty.", "It’s not whether you get knocked down, it’s whether you get up.", "If you have something to do today, don't do it and you will have one free day." };
        string[] greetings = { "Hi sir", "Hi", "Hello, how are you.", "I am realy glad to see you again", "Hey", "Whoops, you think it was lag, nope, it was prank...", "Whats up sir.", "Welcome" };
        public GloraTextAssistant()
        {
            InitializeComponent();
            ss.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Senior);
            name = "jackob";
            //wind.Height = SystemParameters.PrimaryScreenHeight;

            //ss.SelectVoiceByHints(VoiceGender.Male);
            if (File.Exists("programing.txt"))
            {
                using (StreamReader sr = new StreamReader("programing.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "true")
                        {
                            backProgramm.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            CheckIfExistsPinApps();
        }
        private void dictateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tbCommandForPeople.Text != "")
            {
                if (openBrowsers)
                {
                    bool know = false;
                    foreach (var items in browsers)
                    {
                        if (tbCommandForPeople.Text.ToLower().Contains(items))
                        {
                            Process.Start(items);
                            know = true;
                        }
                    }
                    if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                    {
                        ss.SpeakAsync("Type name, I will repeat it.");
                        gloraSay.Items.Add("");
                    }
                    else if (know)
                    {
                        ss.SpeakAsync("I am opnening it.");
                        openBrowsers = false;
                    }
                    else
                    {
                        ss.SpeakAsync("OK");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("OK");
                        openBrowsers = false;
                    }
                }
                else if (cRedirect)
                {
                    if (startC)
                    {
                        if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                        {
                            ss.SpeakAsync("Excelent! Now, I will redirect you to our programmer learner. If somthing will be wrong, conact me Sir!");
                            gloraSay.Items.Add("");
                            gloraSay.Items.Add("Redirect to learner YT chanell.");
                            Stream stream = new FileStream("programing.txt", FileMode.Append);
                            using (StreamWriter sw = new StreamWriter(stream))
                            {
                                sw.WriteLine("true");
                            }
                            Process.Start("chrome", "https://www.youtube.com/watch?v=GjjYlYUsO6s");
                        }
                        else
                        {
                            ss.SpeakAsync("Allright sir!");
                            howToProgram = false;
                            beginerProgrammer = false;
                            pythonRedirect = false;
                            pythonRedirect = false;
                            cRedirect = false;
                            startC = false;
                        }
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                    {
                        Process.Start("chrome", "https://visualstudio.microsoft.com/cs/vs/");
                        ss.SpeakAsync("Click on the download button and download Community version, after successfull download and opening that file type yes.");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("After open Visual Studio - type yes.");
                        startC = true;
                    }
                    else
                    {
                        ss.SpeakAsync("Allright sir!");
                        howToProgram = false;
                        beginerProgrammer = false;
                        pythonRedirect = false;
                        pythonRedirect = false;
                        cRedirect = false;
                    }
                }
                else if (pythonRedirect)
                {
                    if (startPython)
                    {
                        if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                        {
                            ss.SpeakAsync("Nice. Now I will play video for learning.");
                            gloraSay.Items.Add("");
                            gloraSay.Items.Add("Video loading...");
                        }
                        else
                        {
                            ss.SpeakAsync("Allright sir!");
                            howToProgram = false;
                            beginerProgrammer = false;
                            pythonRedirect = false;
                            pythonRedirect = false;
                            startPython = false;
                        }
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                    {
                        Process.Start("chrome", "https://www.python.org/downloads/");
                        ss.SpeakAsync("Click on the yellow button, after success download and opening that file, type YES.");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("After success download, and opening the program, type yes.");
                        startPython = true;
                    }
                    else
                    {
                        ss.SpeakAsync("Allright sir!");
                        howToProgram = false;
                        beginerProgrammer = false;
                        pythonRedirect = false;
                        pythonRedirect = false;
                    }
                }
                else if (beginerProgrammer)
                {
                    if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                    {
                        ss.SpeakAsync("Then I will recomand you to learn Python for first language. Do you want to continue?");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("I will redirect you to the python page. <yes/no>");
                        pythonRedirect = true;
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("done"))
                    {
                        ss.SpeakAsync("Allright sir!");
                        howToProgram = false;
                        beginerProgrammer = false;
                    }
                    else
                    {
                        ss.SpeakAsync("Nice, so I recomanded to you a C#! I will redirect you to the page from microsoft for IDE to program");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("I will redirect you to the microsoft page. <yes/no>");
                        cRedirect = true;
                    }
                }
                else if (howToProgram == true)
                {
                    if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                    {
                        ss.SpeakAsync("I am really glad sir! If you want to leave this mode, type DONE.");
                        ss.SpeakAsync("Programming is hard thing, but you can learn it in some way. Tell me, are you really a beginner?");
                        ss.SpeakAsync("That means you don't know what is array, variable like string, intieger etc ?");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("Tell me, are you really a beginner? <yes/no>");
                        beginerProgrammer = true;
                    }
                    else
                    {
                        ss.SpeakAsync("OK, but you know i can do that!");
                        howToProgram = false;
                    }
                }
                else if (openFile)
                {
                    if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("yeah") || tbCommandForPeople.Text.ToLower().Contains("jop"))
                    {
                        Process.Start(path);
                        ss.SpeakAsync("Sure sir!");
                        shutProgramDown = false;
                        problems = false;
                        openFile = false;
                    }
                    else
                    {
                        ss.SpeakAsync("OK");
                        shutProgramDown = false;
                        problems = false;
                        openFile = false;
                    }
                }
                else if (problems == true)
                {
                    if (cantOpen == true)
                    {
                        ss.SpeakAsync("Sorry I can't shut down it...");
                    }
                    else if (shutProgramDown == true)
                    {
                        Process[] runningProcesses = Process.GetProcesses();
                        foreach (Process process in runningProcesses)
                        {
                            foreach (ProcessModule module in process.Modules)
                            {
                                if (module.FileName.Equals(tbCommandForPeople.Text))
                                {
                                    process.Kill();
                                    ss.SpeakAsync("Process was successfully killed.");
                                    gloraSay.Items.Add("");
                                    gloraSay.Items.Add("Process was successfully killed.");
                                }
                                else
                                {
                                    MessageBox.Show("Sorry, I can not find your file..", "Ehmmm..");
                                }
                            }
                        }
                        shutProgramDown = false;
                        problems = false;
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("processor") || tbCommandForPeople.Text.ToLower().Contains("lag") || tbCommandForPeople.Text.ToLower().Contains("bug"))
                    {
                        problems = false;
                        ss.SpeakAsync("I prefer restart PC, if this not work, you can upgrade your computer. Can I restart your machine?");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("Can I restart your machine? <yes/no>");
                        problemsHelp = true;
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("program") && tbCommandForPeople.Text.ToLower().Contains("shut") || tbCommandForPeople.Text.ToLower().Contains("close"))
                    {
                        shutProgramDown = true;
                        ss.SpeakAsync("Please write exact name of the progrma or file.");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("Please write exact name of the progrma/file.");
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("program") || tbCommandForPeople.Text.ToLower().Contains("open"))
                    {
                        cantOpen = true;
                        ss.SpeakAsync("Please write exact name of the progrma or file.");
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("Please write exact name of the progrma/file.");
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("done"))
                    {
                        problems = false;
                        ss.SpeakAsync("Okay, I believe i help you fine.");
                    }
                    else if (tbCommandForPeople.Text.ToLower().Contains("yes") || tbCommandForPeople.Text.ToLower().Contains("jop") || tbCommandForPeople.Text.ToLower().Contains("of course") || tbCommandForPeople.Text.ToLower().Contains("can do"))
                    {
                        Process.Start("shutdown.exe", "-r");
                        ss.SpeakAsync("Ok, please wait a little moment.");
                    }
                }
                if (tbCommandForPeople.Text.ToLower().Contains("have") && tbCommandForPeople.Text.ToLower().Contains("problem"))
                {
                    problems = true;
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Type something like lag/bug/memory/storage etc..");
                    ss.SpeakAsync("What kind of problem you have? If you want to leave solving problem, type DONE.");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("saving program") || tbCommandForPeople.Text.ToLower().Contains("calculate my savings") || tbCommandForPeople.Text.ToLower().Contains("calculate saving"))
                {
                    ss.SpeakAsync("I am opening my special software for that, sir.");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Opening Savings...");
                    Savings savings = new Savings();
                    savings.Show();
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("hello there"))
                {
                    ss.SpeakAsync("General Kenobi!");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("General Kenobi!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("watch file"))
                {
                    FileW fileW = new FileW();
                    fileW.ShowDialog();
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("how to program") || tbCommandForPeople.Text.ToLower().Contains("learn programming") || tbCommandForPeople.Text.ToLower().Contains("learn program") || tbCommandForPeople.Text.ToLower().Contains("want to program application") || tbCommandForPeople.Text.ToLower().Contains("create application") || tbCommandForPeople.Text.ToLower().Contains("create software") || tbCommandForPeople.Text.ToLower().Contains("create a application") || tbCommandForPeople.Text.ToLower().Contains("create a software") || tbCommandForPeople.Text.ToLower().Contains("create program"))
                {
                    ss.SpeakAsync("If you want to learn how to program, type yes.");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("If you want to learn how to program, type yes.");
                    howToProgram = true;
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("virus") || tbCommandForPeople.Text.ToLower().Contains("check for virus"))
                {
                    Process process = new Process();
                    OpenFileDialog opf = new OpenFileDialog();
                    ss.SpeakAsync("Choose file that you want to scan.");
                    Nullable<bool> res = opf.ShowDialog();
                    if (res == true)
                    {
                        path = opf.FileName;
                    }
                    var processStartInfo = new ProcessStartInfo("C:/Program Files/Windows Defender/MpCmdRun.exe")
                    {
                        Arguments = $"-Scan -ScanType 3 -File \"{path}\" -DisableRemediation",
                        CreateNoWindow = true,
                        ErrorDialog = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = false
                    };
                    process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("No issues found, you can open file.  Can I open it?");
                        ss.SpeakAsync("No issues found, you can open file. Can I open it?");
                        openFile = true;
                    }
                    else
                    {
                        gloraSay.Items.Add("");
                        gloraSay.Items.Add("I found some problems sir....");
                        ss.SpeakAsync("I found some problems sir... You can open it, but i dont recomanded to you that. It can be problem in softwers, which has nothing problems, but the source is not verified.");
                    }
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("read") || tbCommandForPeople.Text.ToLower().Contains("show") || tbCommandForPeople.Text.ToLower().Contains("open"))
                {
                    tbFileOpenInfo.Clear();
                    allTxt = "";
                    ss.SpeakAsync("What kind of file I have to read?");
                    OpenFileDialog opf = new OpenFileDialog();
                    opf.Filter = "Text|*.txt|All|*.*";
                    Nullable<bool> res = opf.ShowDialog();
                    if (res == true)
                    {
                        path = opf.FileName;
                    }
                    if (path == null)
                    { }
                    else
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                ss.SpeakAsync(line);
                                allTxt += line + Environment.NewLine;
                            }
                            tbFileOpenInfo.Text = allTxt;
                        }
                        btnSaveReadingFile.IsEnabled = true;
                        tbFileOpenInfo.IsEnabled = true;
                        gloraSay.Items.Add("You can edit the file.");
                    }
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("thank"))
                {
                    ss.SpeakAsync("I am trying to do my best sir!");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("I am trying to do my best sir!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("how are you"))
                {
                    ss.SpeakAsync("Thanks for asking sir, I am realy fine!");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Thanks for asking sir, I am realy fine!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("play"))
                {
                    string g = tbCommandForPeople.Text.ToLower().Replace(" ", "+");
                    ss.SpeakAsync("Yeah, i am going to play it!");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Yeah, i am going to play it!");
                    int y = g.IndexOf("y") + 1;
                    search = g.Substring(y);
                    Process.Start("chrome", "https://www.youtube.com/results?search_query=" + search);
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("do you know something about"))
                {
                    string g = tbCommandForPeople.Text.Replace(' ', '+');
                    ss.SpeakAsync("Yeah of caurse I know, let you see it.");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Yeah...");
                    int y = g.IndexOf("b") + 4;
                    search = g.Substring(y);

                    string urlAddress = "https://www.google.com/search?q=" + search;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        Stream stream = new FileStream("web.txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(readStream.ReadToEnd());
                        }
                        response.Close();
                        readStream.Close();
                    }
                    webPageCode.IsEnabled = true;

                    Process.Start("chrome", "https://www.google.com/search?q=" + search);
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("hello") || tbCommandForPeople.Text.ToLower().Contains("hi") || tbCommandForPeople.Text.ToLower().Contains("hey") || tbCommandForPeople.Text.ToLower().Contains("glora"))
                {
                    Random random = new Random();
                    int x = random.Next(0, 7);
                    if (greetings[x] == "Whoops, you think it was lag, nope, it was prank...")
                        Thread.Sleep(2500);
                    ss.SpeakAsync(greetings[x]);
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add(greetings[x]);
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("time"))
                {
                    ss.SpeakAsync("current date is " + DateTime.Now.ToLocalTime());
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add(DateTime.Now.ToLocalTime().ToString());
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("how old") && tbCommandForPeople.Text.ToLower().Contains("you"))
                {
                    ss.SpeakAsync("It is not polit to ask about woman's age");
                    gloraSay.Items.Add("It is not polit to ask about woman's age.");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("detect browsers") || tbCommandForPeople.Text.ToLower().Contains("browser"))
                {
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
                    if (registryKey == null)
                        registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");

                    string[] browserNames = registryKey.GetSubKeyNames();
                    ss.SpeakAsync("I detected " + browserNames.Length + " browsers.");
                    foreach (var item in browserNames)
                    {
                        ss.SpeakAsync(item);
                        browsers.Add(item.ToLower());
                    }
                    ss.SpeakAsync("Do you want to open some of them?");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Do you want to open some of them?");
                    openBrowsers = true;
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("current date"))
                {
                    ss.SpeakAsync("current date is " + DateTime.Now.ToLongDateString());
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add(DateTime.Now.ToLongDateString());
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("are you here"))
                {
                    ss.SpeakAsync("I am here sir, dont be afraid");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("I am here sir, don't be afraid!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("motivate me"))
                {
                    ss.SpeakAsync("Lets do it!");
                    Random random = new Random();
                    int y = random.Next(0, 7);
                    ss.SpeakAsync(motivate[y]);
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Let's do it!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("fine"))
                {
                    ss.SpeakAsync("I am glad.");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("I am glad!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("from") && tbCommandForPeople.Text.ToLower().Contains("to"))
                {
                    int y = tbCommandForPeople.Text.IndexOf("m") + 2;
                    string se = tbCommandForPeople.Text.Substring(y);
                    int y2 = se.IndexOf(" ");
                    int y3 = se.IndexOf(" ") + 1;
                    string se2 = se.Substring(y3);
                    int y4 = se2.IndexOf("o") + 2;
                    string se3 = se2.Substring(y4);
                    int x1 = Convert.ToInt32(se.Substring(0, y2));
                    int x2 = Convert.ToInt32(se3);
                    ss.SpeakAsync("Ok sir!");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Counting....");
                    while (x1 != x2)
                    {
                        ss.SpeakAsync(x1.ToString());
                        x1--;
                    }
                    if (x1 == x2)
                    {
                        ss.SpeakAsync("DONE!!!");
                    }
                }

                else if (tbCommandForPeople.Text.ToLower().Contains("how are you"))
                {
                    ss.SpeakAsync("Thanks for asking sir, i am fine, and you?");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Thanks for asking, I am fine, and you?");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("reset"))
                {
                    ss.SpeakAsync("I am reseting myself.?");
                    tbFileOpenInfo.Clear();
                    tbFileOpenInfo.IsEnabled = false;
                    btnSaveReadingFile.IsEnabled = false;
                    gloraSay.Items.Clear();
                    dictateTb.Items.Clear();
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("thank you"))
                {
                    ss.SpeakAsync("pleasure is on my side " + name);
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Pleasure is on my side " + name);
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("browser") || tbCommandForPeople.Text.ToLower().Contains("web") || tbCommandForPeople.Text.ToLower().Contains("google"))
                {
                    ss.SpeakAsync("I am opening google sir!");
                    Process.Start("chrome", "https://www.google.com");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("I am opening google sir!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("close") || tbCommandForPeople.Text.ToLower().Contains("exit"))
                {
                    if (problems == true) { }
                    else
                    {
                        ss.Speak("See you later!");
                        //UPRAVIT
                        //this.Close();
                    }

                }
                else if (tbCommandForPeople.Text.ToLower().Contains("calculator"))
                {
                    ss.Speak("openning calculator");
                    //Process.Start("calculator");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains(".cz") || tbCommandForPeople.Text.ToLower().Contains(".com"))
                {
                    ss.Speak("I am looking for that page " + tbCommandForPeople.Text);

                    string urlAddress = "https://www." + tbCommandForPeople.Text;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }
                        Stream stream = new FileStream("web.txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(readStream.ReadToEnd());
                        }
                        response.Close();
                        readStream.Close();
                    }

                    Process.Start("chrome", "https://www." + tbCommandForPeople.Text);
                    webPageCode.IsEnabled = true;
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("youtube"))
                {
                    ss.SpeakAsync("Opening YouTube sir");
                    Process.Start("chrome", "https://www.youtube.com");
                    gloraSay.Items.Add("");
                    gloraSay.Items.Add("Opening YouTube sir!");
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("search "))
                {
                    string g = tbCommandForPeople.Text.Replace(' ', '+');
                    ss.SpeakAsync("Okay sir, I am searching for it");
                    int y = g.IndexOf("f") + 4;
                    search = g.Substring(y);

                    string urlAddress = "https://www.google.com/search?q=" + search;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        Stream stream = new FileStream("web.txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(readStream.ReadToEnd());
                        }
                        response.Close();
                        readStream.Close();
                    }
                    webPageCode.IsEnabled = true;

                    Process.Start("chrome", "https://www.google.com/search?q=" + search);
                }
                else if (tbCommandForPeople.Text.ToLower().Contains("who is "))
                {
                    string g = tbCommandForPeople.Text.Replace(' ', '+');
                    ss.SpeakAsync("Okay sir, I am searching for it");
                    int y = g.IndexOf("s") + 2;
                    search = g.Substring(y);

                    string urlAddress = "https://www.google.com/search?q=" + search;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        Stream stream = new FileStream("web.txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(readStream.ReadToEnd());
                        }
                        response.Close();
                        readStream.Close();
                    }
                    webPageCode.IsEnabled = true;

                    Process.Start("chrome", "https://www.google.com/search?q=" + search);
                }
                dictateTb.Items.Add("You: " + tbCommandForPeople.Text);
                dictateTb.Items.Add(" ");
                if (indexCommands == 30)
                {
                    for (int i = 0; i < commands.Length - 1; i++)
                    {
                        commands[i] = commands[i + 1];
                    }
                    commands[30] = tbCommandForPeople.Text;
                }
                else
                {
                    commands[indexCommands] = tbCommandForPeople.Text;
                    indexCommands++;
                }
            }
            else { }
            tbCommandForPeople.Text = "";

            if (problemsHelp == true)
            {
                problems = true;
            }
        }

        private void tbCommandForPeople_KeyDown(object sender, KeyEventArgs e)
        {
            int helpIndexCommands = 0;
            if (e.Key == Key.Enter)
            {
                dictateBtn_Click(sender, e);
            }
            else if (e.Key == Key.Up)
            {
                ss.SpeakAsync("Whooops");
                if (indexCommands == 0) { }
                else if (helpIndexCommands == indexCommands)
                {
                    tbCommandForPeople.Text = commands[0];
                }
                else
                {
                    helpIndexCommands++;
                    tbCommandForPeople.Text = commands[indexCommands - helpIndexCommands];
                }
            }
            else if (e.Key == Key.Down)
            {
                if (helpIndexCommands == 0) { }
                else if (helpIndexCommands == indexCommands)
                {
                    helpIndexCommands--;
                    tbCommandForPeople.Text = commands[indexCommands - helpIndexCommands];
                }
            }
        }

        private void webPageCode_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("web.txt");
        }

        private void btnSaveReadingFile_Click(object sender, RoutedEventArgs e)
        {
            Stream stream = new FileStream(path, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.WriteLine(tbFileOpenInfo.Text);
            }
            btnSaveReadingFile.IsEnabled = false;
            ss.SpeakAsync("File was saved successfully.");
        }

        private void tbFileOpenInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FontDialog fd = new System.Windows.Forms.FontDialog();
            System.Windows.Forms.DialogResult dg = fd.ShowDialog();
            if (dg == System.Windows.Forms.DialogResult.OK)
            {
                tbFileOpenInfo.FontFamily = new FontFamily(fd.Font.Name);
                tbFileOpenInfo.FontSize = fd.Font.Size * 96.0 / 72.0;
                tbFileOpenInfo.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                tbFileOpenInfo.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                TextDecorationCollection tdc = new TextDecorationCollection();
                if (fd.Font.Underline) tdc.Add(TextDecorations.Underline);
                if (fd.Font.Strikeout) tdc.Add(TextDecorations.Strikethrough);
                tbFileOpenInfo.TextDecorations = tdc;
            }
        }

        private void pripominky_Click(object sender, RoutedEventArgs e)
        {
            ss.SpeakAsync("I am opening your hard thinkies.");
            Pripominky pripominky = new Pripominky();
            pripominky.Show();
        }

        private void moneySaver_Click(object sender, RoutedEventArgs e)
        {
            ss.SpeakAsync("Enter what you need in the shelves and let the program do what it can.");
            Savings savings = new Savings();
            savings.ShowDialog();
        }

        private void backProgramm_Click(object sender, RoutedEventArgs e)
        {
            ss.SpeakAsync("Now I'm redirecting you to our lecturer. You can choose any lesson you want.");
            Process.Start("chrome", "https://www.youtube.com/watch?v=GjjYlYUsO6s&list=PLc8i1wXj7ZYMUKQ0rDX758dB7gEdKta5Q");
        }

        private void plus1_Click(object sender, RoutedEventArgs e)
        {
            ss.SpeakAsync("You can pin here your favourite application or file sir.");
            Button btn = (Button)sender;
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == true)
            {
                string path = opf.FileName;
                string file = System.IO.Path.GetFileName(path);
                if (btn.Name == "plus1")
                {
                    plus1Btn.Visibility = Visibility.Visible;
                    plus1Btn.Content = file;
                    Stream stream = new FileStream("plus1Btn.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                        sw.WriteLine(path);
                }
                else if (btn.Name == "plus2")
                {
                    plus2Btn.Visibility = Visibility.Visible;
                    plus2Btn.Content = file;
                    Stream stream = new FileStream("plus2Btn.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                        sw.WriteLine(path);
                }
                else if (btn.Name == "plus3")
                {
                    plus3Btn.Visibility = Visibility.Visible;
                    plus3Btn.Content = file;
                    Stream stream = new FileStream("plus3Btn.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                        sw.WriteLine(path);
                }
                else if (btn.Name == "plus4")
                {
                    plus4Btn.Visibility = Visibility.Visible;
                    plus4Btn.Content = file;
                    Stream stream = new FileStream("plus4Btn.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                        sw.WriteLine(path);
                }
                btn.Visibility = Visibility.Hidden;
            }
        }

        private void plus1Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            using (StreamReader sr = new StreamReader(btn.Name + ".txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Process.Start(line);
                }
            }
        }

        public void CheckIfExistsPinApps()
        {
            if (File.Exists(plus1Btn.Name + ".txt"))
            {
                plus1.Visibility = Visibility.Hidden;
                plus1Btn.Visibility = Visibility.Visible;
                using (StreamReader sr = new StreamReader(plus1Btn.Name + ".txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        plus1Btn.Content = System.IO.Path.GetFileName(line);
                    }
                }
            }
            if (File.Exists(plus2Btn.Name + ".txt"))
            {
                plus2.Visibility = Visibility.Hidden;
                plus2Btn.Visibility = Visibility.Visible;
                using (StreamReader sr = new StreamReader(plus2Btn.Name + ".txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        plus2Btn.Content = System.IO.Path.GetFileName(line);
                    }
                }
            }
            if (File.Exists(plus3Btn.Name + ".txt"))
            {
                plus3.Visibility = Visibility.Hidden;
                plus3Btn.Visibility = Visibility.Visible;
                using (StreamReader sr = new StreamReader(plus3Btn.Name + ".txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        plus3Btn.Content = System.IO.Path.GetFileName(line);
                    }
                }
            }
            if (File.Exists(plus4Btn.Name + ".txt"))
            {
                plus4.Visibility = Visibility.Hidden;
                plus4Btn.Visibility = Visibility.Visible;
                using (StreamReader sr = new StreamReader(plus4Btn.Name + ".txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        plus4Btn.Content = System.IO.Path.GetFileName(line);
                    }
                }
            }
        }
    }
}
