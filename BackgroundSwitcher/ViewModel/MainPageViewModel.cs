using BackgroundSwitcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BackgroundSwitcher.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _querry;
        private string _suggestBoxText;
        private List<Subreddit> availableSubreddits;
        private List<Subreddit> _suggestions;
        private ObservableCollection<Subreddit> _selectedSubreddits;
        private int _selectedIndex;
        private bool _removeButtonEnabled;
        private DelegateCommand<string> _removeButtonCommand;

        public MainPageViewModel()
        {
            _removeButtonCommand = new DelegateCommand<string>(
                (s) => { this.removeButton(); });
            selectedIndex = -1;
            availableSubreddits = new List<Subreddit>() {
                // TODO: Add more
                new Subreddit("pics"), new Subreddit("EarthPorn"), new Subreddit("wallpaper")
            };
            suggestions = new List<Subreddit>();
            selectedSubreddits = new ObservableCollection<Subreddit>();
        }

        public string querry
        {
            get
            {
                return this._querry;
            }

            set
            {
                if(value != this._querry)
                {
                    this._querry = value;
                    this.RaisePropertyChanged(nameof(querry));
                    // this._querrySubmittedCommand.RaiseCanExecuteChanged();
                    this.updateSuggestions();
                }
            }
        }

        public string suggestBoxText
        {
            get
            {
                return this._suggestBoxText;
            }

            set
            {
                if(value != this._suggestBoxText)
                {
                    this._suggestBoxText = value;
                    this.RaisePropertyChanged(nameof(suggestBoxText));
                }
            }
        }

        private void updateSuggestions()
        {
            if(string.IsNullOrEmpty(this._querry))
            {
                suggestions.Clear();
            } else
            {
                //suggestions = RedditParser.getSubredditsByName(this._querry);
            }
        }

        public List<Subreddit> suggestions
        {
            get
            {
                return _suggestions;
            }

            set
            {
                if(value != this._suggestions)
                {
                    this._suggestions = value;
                    this.RaisePropertyChanged(nameof(suggestions));
                }
            }
        }

        public ObservableCollection<Subreddit> selectedSubreddits
        {
            get
            {
                return this._selectedSubreddits;
            }

            set
            {
                if(value != this._selectedSubreddits)
                {
                    this._selectedSubreddits = value;
                    this.RaisePropertyChanged(nameof(selectedSubreddits));
                }
            }
        }

        public int selectedIndex
        {
            get
            {
                return this._selectedIndex;
            }

            set
            {
                if(value != this._selectedIndex)
                {
                    this._selectedIndex = value;
                    this.RaisePropertyChanged(nameof(selectedIndex));
                    removeButtonEnabled = _selectedIndex > -1;
                }
            }
        }

        public bool removeButtonEnabled
        {
            get
            {
                return this._removeButtonEnabled;
            }

            set
            {
                if(value != this._removeButtonEnabled)
                {
                    this._removeButtonEnabled = value;
                    this.RaisePropertyChanged(nameof(removeButtonEnabled));
                }
            }
        }

        public void querrySubmitted()
        {
            if(!selectedSubreddits.Contains(new Subreddit(querry)))
                selectedSubreddits.Add(new Subreddit(querry));
            SimpleTextToastManager tm = SimpleTextToastManager.Instance;
            tm.ShowToast("Test", querry + " added!");
            querry = string.Empty;
            suggestBoxText = string.Empty;
        }

        private void removeButton()
        {
            selectedSubreddits.RemoveAt(selectedIndex);
        }

        public DelegateCommand<string> removeButtonCommand
        {
            get
            {
                return _removeButtonCommand;
            }
        }
    }
}
