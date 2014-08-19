using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using System.Reflection;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Factory;
using System.Threading;
using System.Windows.Automation;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var markpadLocation = Path.Combine(directoryName, @"putty.exe");
            Application application = Application.Launch(markpadLocation);
            Window window = application.GetWindow("PuTTY Configuration", InitializeOption.NoCache);
            try
            {
                SearchCriteria searchCriteria = SearchCriteria.ByText("Host Name (or IP address)").AndIndex(1);
                TextBox j = (TextBox)window.Get(searchCriteria);
                j.SetValue("192.168.0.221");
                SearchCriteria searchCriteria2 = SearchCriteria.All.AndIndex(29);
                IUIItem k = window.Get(searchCriteria2);
                k.Click();
                Thread.Sleep(2000);
                List<Window> windows = application.GetWindows();
                foreach (Window w in windows)
                {
                    w.Keyboard.Enter("root\n");
                    //for (int i = 0; i < 100; i++)
                    //{
                        SearchCriteria searchCriteria3 = SearchCriteria.All.AndIndex(0);
                        AutomationElement ae = w.GetElement(searchCriteria3);
                        TreeWalker walker = TreeWalker.ControlViewWalker;
                        AutomationElement parent = walker.GetParent(ae);
                        System.Console.WriteLine(parent.Current.Name);
                        AutomationElementCollection children = parent.FindAll(TreeScope.Descendants, Condition.TrueCondition);
                        foreach (AutomationElement child in children)
                        {
                            if (child.Current.IsContentElement)
                            {
                                AutomationProperty[] aps = child.GetSupportedProperties();
                                foreach (AutomationProperty ap in aps)
                                {                                    
                                    System.Console.WriteLine(ap.GetType() + ":" + ap.ProgrammaticName + ":" + ap.Id);
                                    System.Console.WriteLine(child.GetCurrentPropertyValue(ap).ToString());
                                }

                            }
                        }
                    //}
                }
                /*for (int i = 0; i < 100; i++)
                {
                    SearchCriteria searchCriteria = SearchCriteria.All.AndIndex(i);
                    IUIItem j = window.Get(searchCriteria);
                    System.Console.WriteLine(i + ":" + j.ToString());
                }*/
            }
            catch (AutomationException e)
            {
                System.Console.WriteLine(e.ToString());
            }
            
            /*Button button = (Button)window.Get(searchCriteria);
            button.Click();
            Thread.Sleep(2000);
            List<Window> windows = application.GetWindows();
            foreach (Window w in windows)
            {
                if (w.ToString().Equals("New Task"))
                {
                    SearchCriteria searchCriteria2 = SearchCriteria.All.AndIndex(5);
                    TextBox txtBx = (TextBox)w.Get(searchCriteria2);
                    txtBx.SetValue("New Event");
                }

            }*/
            /* Window window = application.GetWindow("Wpf Todo", InitializeOption.NoCache);

             SearchCriteria searchCriteria = SearchCriteria.All.AndIndex(4);
             Button button = (Button)window.Get(searchCriteria);
             button.Click();
             Thread.Sleep(2000);
             List<Window> windows = application.GetWindows();
             foreach (Window w in windows)
             {
                 if (w.ToString().Equals("New Task"))
                 {
                     SearchCriteria searchCriteria2 = SearchCriteria.All.AndIndex(5);
                     TextBox txtBx = (TextBox)w.Get(searchCriteria2);
                     txtBx.SetValue("New Event");
                 }
                
             }*/
        }
    }
}
