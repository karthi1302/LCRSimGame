using LCRSimGame.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace LCRSimGame
{
    /// <summary>
    /// LCRViewModel
    /// </summary>
    /// <seealso cref="LCRSimGame.ViewModelBase" />
    public class LCRViewModel : ViewModelBase
    {
        /// <summary>
        /// The preset
        /// </summary>
        private List<string> _preset;
        /// <summary>
        /// The turns
        /// </summary>
        private List<int> _turns;
        /// <summary>
        /// The LCR model
        /// </summary>
        private LCRModel _lcrModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LCRViewModel"/> class.
        /// </summary>
        public LCRViewModel()
        {
            _lcrModel = new LCRModel();
            _turns = new List<int>();
            DefaultPresetValue = 0;
            Output = new SilentObservableCollection<KeyValuePair<int, int>>();
            _playerList = new List<Player>();
        }

        /// <summary>
        /// The play command
        /// </summary>
        RelayCommand _playCommand;
        /// <summary>
        /// Gets the play command.
        /// </summary>
        /// <value>
        /// The play command.
        /// </value>
        public ICommand PlayCommand
        {
            get
            {
                if (_playCommand == null)
                {
                    _turns.Clear();
                    _playCommand = new RelayCommand(param =>
                    {
                        RunGame();

                    });

                }
                return _playCommand;
            }
        }

        /// <summary>
        /// Run Game
        /// </summary>
        private async void RunGame()
        {
            _turns.Clear();
            Output.Clear();
            
            var result = await Task.Run(() =>
            {
                //_turns = DiceGame.Play(Players, Games);
                var turnsAndPlayWin = DiceGame.Play(Players, Games);     
                    _turns = turnsAndPlayWin.Select(t => t.Key).ToList();
               
                //Set Winner
                var winner = _lcrModel.GetWinner(turnsAndPlayWin);
                _playerList.Find(w => w.Name == winner.ToString()).Winner = true;
               

                return _lcrModel.SetColumnChart(_turns);
            }, _cancellationToken);

            Shortest = _lcrModel.Operations("Shortest");
            Longest = _lcrModel.Operations("Longest");
            Average = _lcrModel.Operations("Average");
            OnPropertyChanged(nameof(PlayersList));
            var load = Dispatcher.CurrentDispatcher.BeginInvoke(() => Output.AddRange(result));
        }

        /// <summary>
        /// The cancellation token
        /// </summary>
        private CancellationToken _cancellationToken;

        /// <summary>
        /// The cancel command
        /// </summary>
        /// 
        RelayCommand _cancelCommand;
        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(param =>
                    {
                        _cancellationToken = new CancellationToken(true);
                    });

                }
                return _cancelCommand;
            }
        }

        /// <summary>
        /// Gets or sets the default preset value.
        /// </summary>
        /// <value>
        /// The default preset value.
        /// </value>
        public int DefaultPresetValue { get; set; }
        /// <summary>
        /// The games
        /// </summary>
        private int _games;
        /// <summary>
        /// Gets or sets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public int Games
        {
            get
            {
                return _games;
            }
            set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        /// <summary>
        /// The player list
        /// </summary>
        private List<Player> _playerList;
        /// <summary>
        /// Gets or sets the players list.
        /// </summary>
        /// <value>
        /// The players list.
        /// </value>
        public List<Player> PlayersList
        {
            get
            {
                _playerList.Clear();
                for (int p = 1; p <= Players; p++)
                {
                    _playerList.Add(new Player { Name = p.ToString() });
                }
                return _playerList;
            }
            set
            {
                _playerList = value;
            }

        }

        /// <summary>
        /// The players
        /// </summary>
        private int _players;

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public int Players
        {
            get
            {
                return _players;
            }
            set
            {
                _players = value;
                
            }
        }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>
        /// The output.
        /// </value>
        public SilentObservableCollection<KeyValuePair<int, int>> Output { get; set; }

        /// <summary>
        /// Gets or sets the preset.
        /// </summary>
        /// <value>
        /// The preset.
        /// </value>
        public List<string> Preset
        {
            get
            {
                _preset = new List<string>();
                _preset = _lcrModel.Presets;
                return _preset;
            }
            set
            {
                _preset = value;
               
            }
        }

        /// <summary>
        /// The shortest
        /// </summary>
        private string _shortest;
        /// <summary>
        /// Gets or sets the shortest.
        /// </summary>
        /// <value>
        /// The shortest.
        /// </value>
        public string Shortest
        {
            get
            {
                return _shortest;
            }
            set
            {
                _shortest = value;
                OnPropertyChanged(nameof(Shortest));
            }
        }
        /// <summary>
        /// The longest
        /// </summary>
        private string _longest;
        /// <summary>
        /// Gets or sets the longest.
        /// </summary>
        /// <value>
        /// The longest.
        /// </value>
        public string Longest
        {
            get
            {
                return _longest;
            }
            set
            {
                _longest = value;
                OnPropertyChanged(nameof(Longest));
            }
        }

        /// <summary>
        /// The average
        /// </summary>
        private string _average;
        /// <summary>
        /// Gets or sets the average.
        /// </summary>
        /// <value>
        /// The average.
        /// </value>
        public string Average
        {
            get
            {

                return _average;
            }
            set
            {
                _average = value;
                OnPropertyChanged(nameof(Average));
            }
        }

        /// <summary>
        /// The selected preset
        /// </summary>
        private string _selectedPreset;
        /// <summary>
        /// Selected Preset
        /// </summary>
        /// <value>
        /// The selected preset.
        /// </value>
        public string SelectedPreset
        {
            get
            {
                return _selectedPreset;
            }
            set
            {
                _selectedPreset = value;
                var parsedItem = _selectedPreset.Split(" ");
                Players = int.Parse(parsedItem[0]);
                Games = int.Parse(parsedItem[3]);
                OnPropertyChanged(nameof(Players));
                OnPropertyChanged(nameof(Games));
                OnPropertyChanged(nameof(PlayersList));
            }
        }

    }
}
