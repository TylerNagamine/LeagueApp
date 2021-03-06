﻿using LeagueApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using static LeagueApi.LeagueApi;

namespace LeagueApp
{
    public partial class Form1 : Form
    {
        private string apikey;
        private ChampionListDto Champions;
        private ChampionListDto ChampionsInv;
        private MasteryListDto Masteries;
        private RuneListDto Runes;
        private ItemListDto Items;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            apikey = apikeyBox.Text;

            try
            {
                // Get static data
                Champions = Get_Champions(apikey);
                ChampionsInv = Get_Champions(apikey, databyid: true);
                Masteries = Get_Masteries(apikey);
                Runes = Get_Runes(apikey);
                Items = Get_Items(apikey);
                List<string> ver = Get_Version(apikey);
                verLabel.Text = ver.First();

                // Get images/initalize image boxes
                Get_Mastery_Images();
                initPictureBoxes();
            }
            catch (Exception error)
            {
                Show_Error(error.Message);
            }

            // Temp fix of placement errors

            // Add options to combobox
            selectFunctionComboBox.BeginUpdate();
            selectFunctionComboBox.Items.Add("Mastery Page By Summoner Champion");
            selectFunctionComboBox.Items.Add("Mastery Page By Summoner");
            selectFunctionComboBox.Items.Add("Rune Page By Summoner");
            selectFunctionComboBox.EndUpdate();
        }

        // Set apikey locally when box text is changed
        private void apikeyBox_TextChanged(object sender, EventArgs e)
        {
            errorLabel.Hide();
            apikey = apikeyBox.Text;
        }
 
        private void apiLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorLabel.Hide();
            // Send user to Api Key website
            ProcessStartInfo sInfo = new ProcessStartInfo("https://developer.riotgames.com/");
            Process.Start(sInfo);
        }

        // Upon completing entering the desired summoner name,
        // run the functions
        private string smnInput1TextDelta = "";
        private void summonerNameInput1_TextChanged(object sender, EventArgs e)
        {
            if (summonerNameInput1.Text != smnInput1TextDelta &&
                smnInput1TextDelta != "")
            {
                smnInput1TextDelta = "";

                errorLabel.Hide();
                MatchListBox.BeginUpdate();
                MatchListBox.Items.Clear();
                MatchListBox.EndUpdate();
                resetMasteries();
            }
        }

        private void summonerNameInput1_KeyDown(object sender, KeyEventArgs e)
        {
            errorLabel.Hide();
            smnInput1TextDelta = summonerNameInput1.Text;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    long summonerId = Get_Summoner_ID_By_Name(summonerNameInput1.Text, apikey);
                    List<MatchReference> matches = Get_Matches_From_Summoner_ID(summonerId, apikey);

                    // Add items to match listbox
                    MatchListBox.BeginUpdate();
                    MatchListBox.Items.Clear();
                    MatchListBox.DisplayMember = "name";
                    MatchListBox.ValueMember = "id";
                    foreach (MatchReference match in matches)
                    {
                        string champ = ChampionsInv.data[match.champion.ToString()].name;
                        MatchListBox.Items.Add(new MatchListBoxType(champ, match.matchId));
                    }
                    MatchListBox.EndUpdate();

                }
                catch (Exception error)
                {
                    Show_Error(error.Message);
                }
            }
        }

        private void MatchListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorLabel.Hide();
            try
            {
                resetMasteries();
                var match = Get_Match_From_Match_ID(((MatchListBoxType)MatchListBox.SelectedItem).id, apikey);

                // Get correct Id from participantIdentities
                long CorrectSummoner = -1;
                foreach (ParticipantIdentity ident in match.participantIdentities)
                {
                    if (ident.player.summonerName == summonerNameInput1.Text)
                    {
                        CorrectSummoner = ident.participantId;
                        break;
                    }
                }
                // With the Id, find the right participant
                List<Mastery> masteries = null;
                foreach (Participant part in match.participants)
                {
                    if (part.participantId == CorrectSummoner)
                    {
                        masteries = part.masteries;
                        break;
                    }
                }

                // Set rank labels in masteriesPanel control
                foreach (Mastery m in masteries)
                {
                    Control label = masteriesPanel.Controls.Find("l" + m.masteryId, true)[0];
                    label.Text = m.rank.ToString();

                    Control box = masteriesPanel.Controls.Find("box" + m.masteryId, true)[0];

                    MasteryDto mst = Masteries.data[m.masteryId.ToString()];
                    toolTip1.SetToolTip(box, mst.name + "\n\n" + mst.description[m.rank-1]);
                }
            }
            catch
            {
                Show_Error("Mastery information is from an older, unsupported verison of League");
            }
        }

        // Summoner name input box for masteries by summoner name
        private string smnInput2TextDelta = "";
        private void summonerNameInput2_TextChanged(object sender, EventArgs e)
        {
            if (summonerNameInput2.Text != smnInput2TextDelta &&
                smnInput2TextDelta != "")
            {
                smnInput2TextDelta = "";

                masteryPageListBox.BeginUpdate();
                masteryPageListBox.Items.Clear();
                masteryPageListBox.EndUpdate();
                resetMasteries();
            }
        }

        private void summonerNameInput2_KeyDown(object sender, KeyEventArgs e)
        {
            errorLabel.Hide();
            smnInput2TextDelta = summonerNameInput2.Text;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    long smnId = Get_Summoner_ID_By_Name(summonerNameInput2.Text, apikey);
                    Dictionary<string, MasteryPagesDto> test = Get_Summoner_Masteries(smnId, apikey);

                    masteryPageListBox.BeginUpdate();
                    masteryPageListBox.Items.Clear();
                    masteryPageListBox.DisplayMember = "name";
                    masteryPageListBox.ValueMember = "masteries";
                    foreach (KeyValuePair<string, MasteryPagesDto> kv in test)
                    {
                        foreach (MasteryPageDto page in kv.Value.pages)
                        {
                            masteryPageListBox.Items.Add(new MasteryListBoxType(page.name, page.masteries));
                        }
                    }
                    masteryPageListBox.EndUpdate();
                }
                catch (Exception error)
                {
                    Show_Error(error.Message);
                }
            }
        }

        private void masteryPageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorLabel.Hide();
            try
            {
                resetMasteries();
                List<MasteryDto> cur = ((MasteryListBoxType)masteryPageListBox.SelectedItem).masteries;

                foreach (MasteryDto page in cur)
                {
                    Control label = masteriesPanel.Controls.Find("l" + page.id, true)[0];
                    label.Text = page.rank.ToString();
                }
            }
            catch (Exception error)
            {
                Show_Error(error.Message);
            }
        }

        // Get runes by summoner name
        private string smnInput3TextDelta = "";
        private void summonerNameInput3_TextChanged(object sender, EventArgs e)
        {
            if (summonerNameInput3.Text != smnInput3TextDelta &&
                smnInput3TextDelta != "")
            {
                smnInput3TextDelta = "";

                runeBox.BeginUpdate();
                runeBox.Items.Clear();
                runeBox.EndUpdate();
                runePageListBox.BeginUpdate();
                runePageListBox.Items.Clear();
                runePageListBox.EndUpdate();
            }
        }

        private void summonerNameInput3_KeyDown(object sender, KeyEventArgs e)
        {
            errorLabel.Hide();
            smnInput3TextDelta = summonerNameInput3.Text;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    long smnId = Get_Summoner_ID_By_Name(summonerNameInput3.Text, apikey);
                    Dictionary<string, RunePagesDto> runes = Get_Summoner_Runes(smnId, apikey);

                    runeBox.BeginUpdate();
                    runeBox.DisplayMember = "name";
                    runeBox.ValueMember = "runes";
                    foreach (RunePageDto page in runes.First().Value.pages)
                    {
                        runeBox.Items.Add(new RuneListBoxType(page.name, page.slots));
                    }
                    runeBox.EndUpdate();
                }
                catch (Exception error)
                {
                    Show_Error(error.Message);
                }
            }
        }

        private void runeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorLabel.Hide();
            try
            {
                HashSet<RuneSlotDto> runes = ((RuneListBoxType)runeBox.SelectedItem).runes;
                Dictionary<long, int> rank = new Dictionary<long, int>();

                foreach (RuneSlotDto rs in runes)
                {
                    if (rank.Keys.Contains(rs.runeId))
                        rank[rs.runeId]++;
                    else
                        rank.Add(rs.runeId, 1);
                }

                runePageListBox.BeginUpdate();
                runePageListBox.Items.Clear();
                foreach (KeyValuePair<long, int> kv in rank)
                {
                    var test = Runes.data[kv.Key.ToString()];
                    runePageListBox.Items.Add(kv.Value + "x " + test.name);
                }
                runePageListBox.EndUpdate();
            }
            catch (Exception error)
            {
                Show_Error(error.Message);
            }
        }

        // Select the desired function
        private void selectFunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            errorLabel.Hide();

            // Show only the panel of the option selected.  Hide all others
            foreach (Control c in this.Controls)
            {
                if (c is Panel)
                    if (c.Parent == this)
                        c.Visible = false;
                //if (c is ListBox)
                //    ((ListBox)c).Items.Clear();
            }
            if ((string)selectFunctionComboBox.SelectedItem == "Mastery Page By Summoner Champion")
            {
                summonerNameLabel1.Parent = masteryByChampion;
                masteryLabel.Parent = masteryByChampion;
                masteriesPanel.Parent = masteryByChampion;
                masteryByChampion.Visible = true;
            }
            else if ((string)selectFunctionComboBox.SelectedItem == "Mastery Page By Summoner")
            {
                summonerNameLabel1.Parent = summonerMasteriesPanel;
                masteryLabel.Parent = summonerMasteriesPanel;
                masteriesPanel.Parent = summonerMasteriesPanel;
                summonerMasteriesPanel.Visible = true;
            }
            else if ((string)selectFunctionComboBox.SelectedItem == "Rune Page By Summoner")
            {
                summonerNameLabel1.Parent = summonerRunesPanel;
                summonerRunesPanel.Visible = true;
            }
        }

        // Retrieve mastery images from ddragon, save locally
        private void Get_Mastery_Images()
        {
            var url = "http://ddragon.leagueoflegends.com/cdn/5.24.2/img/mastery/";
            var directory = Directory.GetCurrentDirectory() + "\\masteries";
            Version storedVersion;
            Version currentVersion = new Version(Masteries.version);

            // New installation runtime stuff; create directory for image files
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            if (!File.Exists(directory + "\\VERSION.txt"))
            {
                // Create version file
                using (var versionFile = File.OpenWrite(directory + "\\VERSION.txt"))
                {
                    using (var writer = new StreamWriter(versionFile))
                    {
                        writer.Write(Masteries.version);
                    }
                }
                storedVersion = currentVersion;
            }
            // Get version number from version file
            else
            {
                using (var versionFile = File.OpenRead(directory + "\\VERSION.txt"))
                {
                    using (var reader = new StreamReader(versionFile))
                    {
                        storedVersion = new Version(reader.ReadLine().Trim());
                    }
                }
            }


            // Download mastery image files
            foreach (KeyValuePair<string, MasteryDto> mastery in Masteries.data)
            {
                string path = directory + "\\" + mastery.Value.id + ".png";
                if (File.Exists(path) && 
                    storedVersion >= currentVersion)
                    continue;

                var imgUrl = url + mastery.Value.id + ".png";
                
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(imgUrl, path);
                    }
                }
                catch (Exception e)
                {
                    Show_Error(e.Message);
                }
            }
        }

        private void initPictureBoxes()
        {
            foreach(KeyValuePair<string, MasteryDto> m in Masteries.data)
            {
                Image image = Image.FromFile(Directory.GetCurrentDirectory() +
                     "\\masteries\\" + m.Value.id + ".png");
                Control cur = masteriesPanel.Controls.Find("box" + m.Value.id.ToString(), true)[0];
                ((PictureBox)cur).Image = image;
                ((PictureBox)cur).Height = image.Height;
                ((PictureBox)cur).Width = image.Width;
                ((PictureBox)cur).BorderStyle = BorderStyle.FixedSingle;

                toolTip1.SetToolTip(cur, m.Value.name + "\n\n" + 
                    m.Value.description.First().Replace("<br>", "\n"));
            }
        }

        private void resetMasteries()
        {
            foreach (KeyValuePair<string, MasteryDto> kv in Masteries.data)
            {
                Control c = masteriesPanel.Controls.Find("l" + kv.Value.id, true)[0];
                c.Text = "0";

                toolTip1.SetToolTip(c, kv.Value.name + "\n\n" +
                    kv.Value.description.First().Replace("<br>", "\n"));
            }
        }

        private void Show_Error(string error)
        {
            errorLabel.Text = error;
            errorLabel.Show();
        }
    }

    class MatchListBoxType
    {
        public string name { get; set; }
        public long id { get; set; }
        public MatchListBoxType(string name, long id)
        {
            this.name = name;
            this.id = id;
        }
    }

    class MasteryListBoxType
    {
        public string name { get; set; }
        public List<MasteryDto> masteries { get; set; }
        public MasteryListBoxType (string name, List<MasteryDto> masteries)
        {
            this.name = name;
            this.masteries = masteries;
        }
    }

    class RuneListBoxType
    {
        public string name { get; set; }
        public HashSet<RuneSlotDto> runes { get; set; }
        public RuneListBoxType(string name, HashSet<RuneSlotDto> runes)
        {
            this.name = name;
            this.runes = runes;
        }
    }
}
