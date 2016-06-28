using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardGameListenServer;
using CardGameServer;
using CardGameTestUI.Properties;
using CardProtocolLibrary;

namespace CardGameTestUI
{
    public partial class frmMain : Form
    {
        private TcpClient rawSocket;
        private Client client;

        private const string IP = "localhost";
        private const int port = 4020;
        private int _pingCounter;

        public ConnectionPhase Phase;

        public event Action SetupPhase;

        public void OnSetupPhase()
        {
            SetupPhase?.Invoke();
        }

        public event Action GamePhase;

        public void OnGamePhase()
        {
            GamePhase?.Invoke();
        }


        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupPhase += Setup;
            GamePhase += GameStart;
        }

        private void GameStart()
        {
            
        }

        private void Setup()
        {
            var deck = new string[Deck.CardLimit];
            deck[0] = "cast_fireball";
            deck[1] = "cast_fireball";
            for (var i = 2; i < Deck.CardLimit; i+=2)
            {
                deck[i] = "summon_testbro" + i/2;
                deck[i+1] = "summon_testbro" + i/2;
            }

            Console.WriteLine(deck);

            client.Writer.SendAction(GameAction.Meta, new Dictionary<string, GameData>
            {
                {"deck", string.Join(",", deck) }
            });
        }

        public void SendAction(GameAction action, Dictionary<string, GameData> data)
        {
            client.Writer.SendAction(action, data);
            lbSent.Items.Add($"{action} - {data}");
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            try
            {
                rawSocket = new TcpClient(cmbIP.Text, port);
                client = new Client(rawSocket);

                SendAction(GameAction.Meta, new Dictionary<string, GameData>
                {
                    {"name", txtName.Text},
                    {"protocol", GameActionWriter.PROTOCOL_VERSION.ToString()}
                });

                tmrPing.Enabled = true;
                lblStatus.ForeColor = Color.GreenYellow;
                lblStatus.Text = Resources.ConnectedString;
                Task.Factory.StartNew(() => GetData(client), TaskCreationOptions.LongRunning);
            }
            catch (Exception ex)
            {
                lbRecieved.Items.Add(ex.Message);
                lblStatus.ForeColor = Color.Firebrick;
                lblStatus.Text = Resources.ConnectionFailed;
                if (rawSocket != null && rawSocket.Connected)
                {
                    rawSocket.Close();
                }
            }
        }

        private void AddLine(string s)
        {
            if (lbRecieved.InvokeRequired)
            {
                lbRecieved.Invoke((MethodInvoker)(() => lbRecieved.Items.Add(s)));
            }
            else
            {
                lbRecieved.Items.Add(s);
            }
        }

        private void AddLine(GameDataAction input)
        {
            var sb = new StringBuilder();
            sb.Append($"{input.Action} : ");
            foreach (var kvp in input.Data)
            {
                sb.Append($"['{kvp.Key}' = {kvp.Value}] ");
            }
            AddLine(sb.ToString());
        }

        private async void GetData(Client c)
        {
            while (c.RawClient.Connected)
            {
                var line = await c.Reader.ReadLineAsync();
                var input = new GameDataAction(line);
                AddLine(input);

                if (input.Action == GameAction.Ping)
                {
                    if (input.Data["counter"].Int() == (++_pingCounter))
                    {
                        AddLine("Ping Success.");
                    }
                    else
                    {
                        AddLine("WARNING: Ping Failure!!!!!!");
                    }
                }
                else if (input.Action == GameAction.Meta)
                {
                    if (input.Data.ContainsKey("phase"))
                    {
                        Phase = input.Data["phase"];
                        switch (Phase)
                        {
                            case ConnectionPhase.Handshake:
                                // This shouldn't occur, is the starting phase.
                                break;
                            case ConnectionPhase.Setup:
                                OnSetupPhase();
                                break;
                            case ConnectionPhase.Game:
                                OnGamePhase();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
            lbRecieved.Items.Add("Disconnected.");
        }

        private void tmrPing_Tick(object sender, EventArgs e)
        {
            if (rawSocket.Connected)
            {
                SendAction(GameAction.Ping, new Dictionary<string, GameData> {{"counter", (++_pingCounter)}});
            }
        }
    }
}
