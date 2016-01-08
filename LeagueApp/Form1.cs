using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using LeagueApi;
using System.Threading;
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
                Champions = Get_Champions(apikey);
                ChampionsInv = Get_Champions(apikey, databyid: true);
                Masteries = Get_Masteries(apikey);
                Runes = Get_Runes(apikey);
                Items = Get_Items(apikey);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error initializing locals",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            selectFunctionComboBox.BeginUpdate();
            selectFunctionComboBox.Items.Add("Mastery Page By Summoner Champion");
            selectFunctionComboBox.Items.Add("Mastery Page By Summoner");
            selectFunctionComboBox.Items.Add("Rune Page By Summoner");
            selectFunctionComboBox.EndUpdate();
        }

        private void apikeyBox_TextChanged(object sender, EventArgs e)
        {
            apikey = apikeyBox.Text;
        }

        private void apiLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://developer.riotgames.com/");
            Process.Start(sInfo);
        }

        private void summonerNameInput1_LostFocus(object sender, EventArgs e)
        {
            try
            {
                long summonerId = Get_Summoner_ID_By_Name(summonerNameInput1.Text, apikey);
                List<MatchReference> matches = Get_Matches_From_Summoner_ID(summonerId, apikey);
                
                MatchListBox.BeginUpdate();
                MatchListBox.Items.Clear();
                MatchListBox.DisplayMember = "name";
                MatchListBox.ValueMember = "id";
                foreach (MatchReference match in matches)
                {
                    var champ = ChampionsInv.data[match.champion.ToString()].name;
                    MatchListBox.Items.Add(new MatchListBoxType(champ, match.matchId));
                }
                MatchListBox.EndUpdate();
                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Summoner name error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void summonerNameInput1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                summonerNameInput1_LostFocus(sender, e);
        }

        private void MatchListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var match = Get_Match_From_Match_ID(long.Parse(((MatchListBoxType)MatchListBox.SelectedItem).id.ToString()), apikey);

                long CorrectSummoner = -1;
                foreach (ParticipantIdentity ident in match.participantIdentities)
                {
                    if (ident.player.summonerName == summonerNameInput1.Text)
                    {
                        CorrectSummoner = ident.participantId;
                        break;
                    }
                }
                List<Mastery> masteries = null;
                foreach (Participant part in match.participants)
                {
                    if (part.participantId == CorrectSummoner)
                    {
                        masteries = part.masteries;
                        break;
                    }
                }

                masteryListBox.BeginUpdate();
                masteryListBox.Items.Clear();
                foreach(Mastery m in masteries)
                {
                    if (m.masteryId < 6000)
                        masteryListBox.Items.Add(m.masteryId.ToString() + "    " + m.rank);
                    else
                    {
                        var mastery = Masteries.data[m.masteryId.ToString()].name;
                        masteryListBox.Items.Add(mastery + "   " + m.rank);
                    }
                }
                masteryListBox.EndUpdate();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error getting match information",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectFunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is Panel)
                    c.Visible = false;
            }
            if ((string)selectFunctionComboBox.SelectedItem == "Mastery Page By Summoner Champion")
            {
                masteryByChampion.Visible = true;
            }
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
}
