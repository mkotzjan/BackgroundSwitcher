using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundSwitcher.Model
{
    public class Subreddit : BaseModel, IEquatable<Subreddit>
    {
        private string _name;
        private string _id;
        private bool _valid;

        public Subreddit(string name)
        {
            this.name = name;
            this._id = name.ToLower();
            this.valid = true;
        }
        
        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this._id = value.ToLower();
                    this.RaisePropertyChanged(nameof(name));
                }
            }
        }

        public string getId()
        {
            return _id;
        }

        public override string ToString()
        {
            return string.Format("{0}", name);
        }

        public bool Equals(Subreddit other)
        {
            return name.Equals(other.name);
        }

        public bool valid
        {
            get
            {
                return this._valid;
            }

            set
            {
                if(value != this._valid)
                {
                    this._valid = value;
                    this.RaisePropertyChanged(nameof(valid));
                }
            }
        }
    }
}
