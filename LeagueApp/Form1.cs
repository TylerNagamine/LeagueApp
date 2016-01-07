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
            selectFunctionComboBox.Items.Add("Mastery Page By Summoner");
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
                foreach (MatchReference match in matches)
                {
                    MatchListBox.Items.Add(match.matchId.ToString());
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
                var match = Get_Match_From_Match_ID(long.Parse(MatchListBox.SelectedItem.ToString()), apikey);

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
                    masteryListBox.Items.Add(m.masteryId.ToString() + "    " + m.rank);
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
            if (selectFunctionComboBox.SelectedText == "Mastery Page By Summoner")
            {
                masterySectionLabel.Visible = true;
                summonerNameLabel1.Visible = true;
                summonerNameInput1.Visible = true;
                selectMatchLabel1.Visible = true;
                MatchListBox.Visible = true;
                getMasteriesButton1.Visible = true;
            }
        }
    }
}
