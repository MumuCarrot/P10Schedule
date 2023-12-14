using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Schedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialize Main Window components
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Transport List, contains all transport which is loaded in memory
        /// </summary>
        private readonly List<Transport> tranList = new List<Transport>();
        /// <summary>
        /// Clear shedule
        /// </summary>
        private void Clear() 
        {
            TransportName.Clear();
            WeekdayTime.Children.Clear();
            WeekendTime.Children.Clear();
        }
        /// <summary>
        /// Add button make settings filed visible
        /// </summary>
        private void CreateNewObjectOfSchedule(object sender, RoutedEventArgs e)
        {
            Clear();
            ScheduleDescription.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Add weekday time
        /// </summary>
        private void CreateNewTimeOfObjectWeekday(object sender, RoutedEventArgs e)
        {
            TextBox newTextBox = new TextBox()
            {
                Height = AddTimeWeekday.Height -5,
                FontSize = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                MinWidth = 79,
                MaxWidth = 79,
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x14, 0x13, 0x21)),
                SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0x36, 0xF5)),
                Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x8C, 0x27)),
                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x4B, 0x4B, 0x4C))
            };
            newTextBox.PreviewTextInput += NewTextBox_PreviewTextInput;

            WeekdayTime.Children.Add(newTextBox);
        }
        /// <summary>
        /// Add weekend time
        /// </summary>
        private void CreateNewTimeOfObjectWeekend(object sender, RoutedEventArgs e)
        {
            TextBox newTextBox = new TextBox()
            {
                Height = AddTimeWeekend.Height,
                FontSize = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                MinWidth = 79,
                MaxWidth = 79,
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x14, 0x13, 0x21)),
                SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0x36, 0xF5)),
                Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x8C, 0x27)),
                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x4B, 0x4B, 0x4C))
            };
            newTextBox.PreviewTextInput += NewTextBox_PreviewTextInput;

            WeekendTime.Children.Add(newTextBox);
        }
        /// <summary>
        /// Copy shedule to text
        /// </summary>
        private void CopyTextTo(object sender, RoutedEventArgs e)
        {
            if (tranList.Count > 0)
            {
                string str = string.Empty;
                foreach (var l in tranList)
                {
                    str += l.ParseSchedule();
                }
                Clipboard.SetData(DataFormats.Text, (Object)str);
            }
        }
        /// <summary>
        /// Text Boxes time settings
        /// </summary>
        private void NewTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Text.ToCharArray()[0] >= '0' && e.Text.ToCharArray()[0] <= '9')
                    switch (textBox.Text.Length)
                    {
                        case 0:
                            if (!(e.Text.ToCharArray()[0] >= '0' && e.Text.ToCharArray()[0] <= '2'))
                            {
                                textBox.Text += "0";
                                textBox.CaretIndex = textBox.Text.Length;
                            }
                            break;
                        case 1:
                            if (textBox.Text.ToCharArray()[0] == '2')
                            {
                                if (!(e.Text.ToCharArray()[0] >= '0' && e.Text.ToCharArray()[0] <= '3'))
                                {
                                    e.Handled = true;
                                }
                            }
                            break;
                        case 2:
                            if (e.Text.ToCharArray()[0] >= '0' && e.Text.ToCharArray()[0] <= '5')
                            {
                                textBox.Text += ":";
                                textBox.CaretIndex = textBox.Text.Length;
                            }
                            else e.Handled = true;
                            break;
                        case 3:
                            break;
                        case 4:
                            if (!(e.Text.ToCharArray()[0] >= '0' && e.Text.ToCharArray()[0] <= '9'))
                            {
                                e.Handled = true;
                            }
                            break;
                        default:
                            e.Handled = true;
                            break;
                    }
                else e.Handled = true;
            }
        }
        /// <summary>
        /// Cancel button
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            ScheduleDescription.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Save button
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransportName.Text.Length > 0) 
            { 
                var newButton = new BuffedButton(TransportName.Text)
                {
                    Height = AddButton.Height,
                    Content = TransportName.Text,
                    FontSize = 20,
                    Margin = new Thickness(5, 0, 5, 0),
                    BorderBrush = null,
                    Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x14, 0x13, 0x21)),
                    Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x8C, 0x27))
                };
                newButton.Click += OpenCurrent_Click;

                List<TimeS> LWday = new List<TimeS>();
                List<TimeS> LWend = new List<TimeS>();

                foreach (UIElement i in WeekdayTime.Children)
                {
                    if (i is TextBox t)
                    {
                        TimeS ts = new TimeS(t.Text);
                        if (!Regex.IsMatch(t.Text, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")) continue;
                        if (!LWday.Contains(ts))
                        {
                            LWday.Add(ts);
                        }
                    }
                }

                foreach (UIElement i in WeekendTime.Children)
                {
                    if (i is TextBox t)
                    {
                        TimeS ts = new TimeS(t.Text);
                        if (!Regex.IsMatch(t.Text, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")) continue;
                        if (!LWend.Contains(ts))
                        {
                            LWend.Add(ts);
                        }
                    }
                }

                var transport = new Transport()
                {
                    Name = TransportName.Text,
                    TimeScheduleWeekdays = LWday,
                    TimeScheduleWeekends = LWend,
                };

                bool tranName = false;
                var trt = new Transport();
                foreach (var i in tranList) 
                {
                    if (i.Name == transport.Name)
                    {
                        trt = i;
                        tranName = true;
                        break;
                    } 
                }

                if (tranName)
                { 
                    tranList.Remove(trt);

                    bool bsc = false;
                    var btn = new Button();
                    foreach (UIElement i in ButtonStack.Children) 
                    {
                        if (i is Button button) 
                        {
                            if (button.Content.ToString() == transport.Name) 
                            {
                                bsc = true;
                                btn = button;
                                break;
                            }
                        }
                    }
                    if (bsc) 
                    {
                        ButtonStack.Children.Remove(btn);
                    }
                }
                tranList.Add(transport);
                ButtonStack.Children.Add(newButton);

                Clear();
                ScheduleDescription.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Opens created element
        /// </summary>
        /// <exception cref="Exception">
        /// If sender is not BuffedButton, exception will occure.
        /// </exception>
        private void OpenCurrent_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            if (sender is BuffedButton button)
            {
                foreach (var i in tranList)
                {
                    if (i.Name.Equals(button.line))
                    {
                        TransportName.Text = i.Name;
                        foreach (var tsweek in i.TimeScheduleWeekdays)
                        {
                            TextBox newTextBox = new TextBox()
                            {
                                Height = AddTimeWeekday.Height,
                                FontSize = 25,
                                Text = tsweek.time,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                MinWidth = 79,
                                MaxWidth = 79,
                                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x14, 0x13, 0x21)),
                                SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0x36, 0xF5)),
                                Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x8C, 0x27)),
                                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x4B, 0x4B, 0x4C))
                            };
                            newTextBox.PreviewTextInput += NewTextBox_PreviewTextInput;

                            WeekdayTime.Children.Add(newTextBox);
                        }
                        foreach (var tsweek in i.TimeScheduleWeekends)
                        {
                            TextBox newTextBox = new TextBox()
                            {
                                Height = AddTimeWeekend.Height,
                                FontSize = 25,
                                Text = tsweek.time,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                MinWidth = 79,
                                MaxWidth = 79,
                                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x14, 0x13, 0x21)),
                                SelectionBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0x36, 0xF5)),
                                Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x8C, 0x27)),
                                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x4B, 0x4B, 0x4C))
                            };
                            newTextBox.PreviewTextInput += NewTextBox_PreviewTextInput;

                            WeekendTime.Children.Add(newTextBox);
                        }
                        break;
                    }
                }
                ScheduleDescription.Visibility = Visibility.Visible;
            }
            else throw new Exception("Sender is not a BuffedButton.");
        }
        /// <summary>
        /// Drag and move
        /// </summary>
        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        /// <summary>
        /// Close button
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Minimization button
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Maximization button
        /// </summary>
        private void MaximizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MainGrid.Margin = new Thickness(0);
            }
            else
            {
                WindowState = WindowState.Maximized;
                MainGrid.Margin = new Thickness(5, 5, 5, 50);
            }
        }
    }
}
