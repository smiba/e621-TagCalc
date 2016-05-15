using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace e621_Tag_Calc
{
    public partial class Form1 : Form
    {
        private List<Results> FavoriteResults;
        private string AllTags = "";

        public class CreatedAt
        {
            public string json_class { get; set; }
            public int s { get; set; }
            public int n { get; set; }
        }

        public class Results
        {
            public int id { get; set; }
            public string tags { get; set; }
            public string description { get; set; }
            public CreatedAt created_at { get; set; }
            public int creator_id { get; set; }
            public string author { get; set; }
            public int change { get; set; }
            public string source { get; set; }
            public int score { get; set; }
            public int fav_count { get; set; }
            public string md5 { get; set; }
            public int file_size { get; set; }
            public string file_url { get; set; }
            public string file_ext { get; set; }
            public string preview_url { get; set; }
            public int preview_width { get; set; }
            public int preview_height { get; set; }
            public string sample_url { get; set; }
            public int? sample_width { get; set; }
            public int? sample_height { get; set; }
            public string rating { get; set; }
            public bool has_children { get; set; }
            public string children { get; set; }
            public int? parent_id { get; set; }
            public string status { get; set; }
            public int? width { get; set; }
            public int? height { get; set; }
            public bool has_comments { get; set; }
            public bool has_notes { get; set; }
            public List<object> artist { get; set; }
            public List<string> sources { get; set; }
            public string delreason { get; set; }
        }

        Thread t;
        int dots = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            TagList.Items.Clear();
            AllTags = "";
            StartBtn.Enabled = false;
            UsernameTxtbox.Enabled = false;
            StartBtn.Text = "Init (1/2)";

            t = new Thread(CollectTags);
            t.Start();
            Thread.Sleep(50); //Give the thread a small amount of time to load / start (Not sure if nessesary)
        }

        private void CollectTags()
        {
            Boolean stop = false; //Mark if the json is empty
            string jsoncode = "[]";
            int page = 1;

            do
            {
                jsoncode = "[]";
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:49.0) Gecko/20100101 Firefox/49.0"); //e621 blocks empty User-Agents (?)
                    string downloadstring = "https://e621.net/post/index.json?tags=fav:" + UsernameTxtbox.Text + "&page=" + page;
                    Debug.WriteLine(downloadstring);
                    jsoncode = client.DownloadString(downloadstring);
                }
                try
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { StartBtn.Text = "Init (2/2)"; });
                    FavoriteResults = JsonConvert.DeserializeObject<List<Results>>(jsoncode);
                    if (FavoriteResults.Count == 0) { stop = true; }
                }
                catch
                {
                    stop = true;
                }
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate()
                {
                    StartBtn.Text = "Loading.";
                    CheckThreadStatus.Enabled = true;
                    CheckThreadStatus.Start();
                });
                page++;

                if (stop == false)
                {               
                    
                    int i = 0;

                    do
                    {
                        string tagjson = FavoriteResults[i].tags;

                        string[] tags = tagjson.Split(' ');
                        foreach (string tag in tags)
                        {
                            AddToListbox(tag);
                        }
                        i++;
                    } while (i < FavoriteResults.Count);

                    if (FavoriteResults.Count < 100)
                    {
                        stop = true;
                    }
                }

                int i2 = 0;
                do
                {
                    if (FavoriteResults.Count > 0) //Check if there are any results, otherwise do not proceed
                    {
                        AllTags += FavoriteResults[i2].tags + " ";
                        i2++;
                    }
                    else { stop = true;  }
                } while (i2 < FavoriteResults.Count);
            } while (stop == false);

            if (AllTags != "") { StartCounting(); }
        }

        private void StartCounting()
        {
            string[] itemsarray = new string[TagList.Items.Count];

            for (int count = 0; count < TagList.Items.Count; count++)
            {
                itemsarray[count] = TagList.Items[count].ToString();
            }

            var items = new System.Collections.ArrayList(TagList.Items);
            string[] FinalTags = new string[TagList.Items.Count];
            int[] FinalTagsCount = new int[TagList.Items.Count];

            //Populate string and init array

            int i = 0;
            do
            {
                FinalTags[i] = itemsarray[i];
                FinalTagsCount[i] = 0;
                i++;
            } while (i < TagList.Items.Count);

            foreach (string tag in items)
            {
                    string[] tags = AllTags.Split(' ');

                    foreach (string tag2 in tags)
                    {
                        if (tag == tag2)
                        {
                            int i2 = 0;
                            do
                            {
                                if (FinalTags[i2] == tag)
                                {
                                    FinalTagsCount[i2]++;
                                }
                                i2++;
                            } while (i2 < TagList.Items.Count);
                        }
                    }
            }

            Boolean Cont = false;
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { TagList.Items.Clear(); Cont = true; });
            do
            {

            } while (Cont == false);

            i = 0;
            do
            {
                int i2 = 0;
                string[] BiggestTag = new string[3];
                BiggestTag[0] = "";
                BiggestTag[1] = "-1";
                BiggestTag[2] = "-1";
                do
                {
                    if (FinalTagsCount[i2] > Convert.ToInt32(BiggestTag[1]))
                    {
                        BiggestTag[2] = i2.ToString();
                        BiggestTag[1] = FinalTagsCount[i2].ToString();
                        BiggestTag[0] = FinalTags[i2];
                    }
                    i2++;
                } while (i2 < FinalTags.Length);

                AddToListbox(FinalTags[Convert.ToInt32(BiggestTag[2])] + " (" + FinalTagsCount[Convert.ToInt32(BiggestTag[2])] + ")");
                FinalTagsCount[Convert.ToInt32(BiggestTag[2])] = -1;
                i++;
            } while (i < FinalTags.Length);
        }

        private void AddToListbox(string toadd)
        {
            Boolean Cont = false;
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() {
                Boolean found = false;

                if (TagList.Items.Contains(toadd))
                {
                    found = true;
                }

                if (found == false)
                {
                    TagList.Items.Add(toadd);
                }
                Cont = true;
            });
            do
            {

            } while (Cont == false);
        }

        private void CheckThreadStatus_Tick(object sender, EventArgs e)
        {

            if (t.ThreadState == System.Threading.ThreadState.Running)
            {
                if (dots == 1)
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { StartBtn.Text = "Loading."; });
                }
                else if (dots == 2)
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { StartBtn.Text = "Loading.."; });
                }
                else if (dots == 3)
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { StartBtn.Text = "Loading..."; });
                    dots = 0;
                }
                dots++;
                Thread.Sleep(125);
            }
            else
            {
                dots = 1;
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() {
                    StartBtn.Text = "Go!";
                    StartBtn.Enabled = true;
                    UsernameTxtbox.Enabled = true;
                });
                CheckThreadStatus.Stop();
                CheckThreadStatus.Enabled = false;
            }
        }
    }
}
