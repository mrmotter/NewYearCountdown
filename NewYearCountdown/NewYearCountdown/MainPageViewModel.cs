using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;

namespace NewYearCountdown
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private TimeSpan _countdown;
        // Get date of next new year by inserting current year + 1.
        private DateTime _newYear = new DateTime(DateTime.Now.Year + 1, 1, 1, 0, 0, 0);
        private int _days;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private string _textColor = "#0066ff";
        Color color;

        // Trackers to follow when the scales hit max and need to go back down or up.
        private bool _decrementRed = false;
        private bool _decrementBlue = false;
        private bool _decrementGreen = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            // Set initial values to be random so they don't constantly follow each other.
            Red = 0;
            Blue = 0.9;
            Green = 0.7;

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Countdown = _newYear - DateTime.Now;
                Days = Countdown.Days;
                Hours = Countdown.Hours;
                Minutes = Countdown.Minutes;
                Seconds = Countdown.Seconds;
                return true;
            });

            Increment();
        }

        // Define incrementers for the colors.
        public void Increment()
        {
            incGreen();
            incBlue();
            incRed();
        }

        public void incRed()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
            {
                if (Red > 0.3)
                    _decrementRed = true;
                if (Red == 0)
                    _decrementRed = false;

                if (_decrementRed)
                    Red -= 0.01;
                else
                    Red += 0.01;

                return true;
            });
        }

        public void incGreen()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
            {
                if (Green > 0.3)
                    _decrementGreen = true;
                else if (Green == 0)
                    _decrementGreen = false;

                if (_decrementGreen)
                    Green -= 0.01;
                else
                    Green += 0.01;

                return true;
            });
        }

        public void incBlue()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (Blue == 1)
                    _decrementBlue = true;
                else if (Blue == 0)
                    _decrementBlue = false;

                if (_decrementBlue)
                    Blue -= 0.01;
                else
                    Blue += 0.01;

                return true;
            });
        }

        // Define properties with their getters and setters.
        public TimeSpan Countdown
        {
            set
            {
                if (_countdown != value)
                {
                    _countdown = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs("DateTime"));
                }
            }
            get
            {
                return _countdown;
            }
        }

        public string TextColor
        {
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs("TextColor"));
                }
            }
            get
            {
                return _textColor;
            }
        }

        public int Days
        {
            set
            {
                if (_days != value)
                {
                    _days = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs("Days"));
                }
            }
            get
            {
                return _days;
            }
        }

        public int Hours
        {
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs("Hours"));
                }
            }
            get
            {
                return _hours;
            }
        }

        public int Minutes
        {
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs("Minutes"));
                }
            }
            get
            {
                return _minutes;
            }
        }

        public int Seconds
        {
            set
            {
                if (_seconds != value)
                {
                    _seconds = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs("Seconds"));
                }
            }
            get
            {
                return _seconds;
            }
        }

        public double Red
        {
            set
            {
                if (color.R != value)
                {
                    Color = new Color(value, color.G, color.B);
                }
            }
            get
            {
                return color.R;
            }
        }

        public double Green
        {
            set
            {
                if (color.G != value)
                {
                    Color = new Color(color.R, value, color.B);
                }
            }
            get
            {
                return color.G;
            }
        }

        public double Blue
        {
            set
            {
                if (color.B != value)
                {
                    Color = new Color(color.R, color.G, value);
                }
            }
            get
            {
                return color.B;
            }
        }

        // Parent property that ensures property changes further down.
        public Color Color
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Red"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Green"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Blue"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
                }
            }
            get
            {
                return color;
            }
        }
    }
}
