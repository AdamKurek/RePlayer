using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Animation;
using System.Linq;

namespace RePlayer

{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window, INotifyPropertyChanged
	{

		private static double[] speeds = { 0.0625, 0.125, 0.25, 0.5, 0.75, 1, 1.25, 1.5, 1.75, 2, 4, 8 };
		private int speedIndex = 5;
		private double time;

		public double TimeSlider
		{
			get
			{
				return time;
			}
			set
			{
				time = value;
				update = true;
				//player.Position = new TimeSpan(0, 0, 0, 0, (int)value);
				PropertyChanged(this, new PropertyChangedEventArgs("TimeSlider"));
				if (player.NaturalDuration.HasTimeSpan)
					Status.Content = String.Format("{0} / {1}", player.Position.ToString(@"mm\:ss"), player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
				
			}
		}

		public double Time
		{
			get
			{
				return time;
			}
			set
			{
				time = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Time"));
					PropertyChanged(this, new PropertyChangedEventArgs("TimeSlider"));
				}
			}
		}
		public double Volume
		{
            get		
			{
				return player.Volume*100;
			}
            set 
			{
				volumeSlider.Visibility = Visibility.Visible;
				VolumeSliderTimer.Stop();
				VolumeSliderTimer.Start();
				if(value < 0)
					player.Volume = 0;
				else if (value >= 100)
					player.Volume = 1;
				else
				player.Volume = value/100;
				PropertyChanged(this, new PropertyChangedEventArgs("Volume"));
			}
		}
	
		private bool hud = true;
		private bool update = false;
		private bool playing = false;
		private bool fullscreen = false;
		private DispatcherTimer DoubleClickTimer = new DispatcherTimer()
		{
			Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime()) 

		};
		private DispatcherTimer VolumeSliderTimer = new DispatcherTimer();
		private DispatcherTimer timer = new DispatcherTimer();
		public string FileName
		{
            get 
			{
				try { 
					return FilePath.Substring((int)((FilePath?.LastIndexOf("\\"))+1));
				}
				catch(Exception ex)
				{
					return "";
				}
            }
		}
		private string filePath;
		public string FilePath 
		{
			get { return filePath; }
			set 
			{
				filePath = value;
				load_video();
				LoadVideoTimmer();
				if(PropertyChanged != null) 
				{ 
					PropertyChanged(this, new PropertyChangedEventArgs(nameof(FilePath)));
			 		PropertyChanged(this, new PropertyChangedEventArgs(nameof(FileName)));
				}
			}
		}

		private string directoryPath = @"C:/Users/Kurek/Videoss";
		public string DirectoryPath 
		{
		get 
			{ 
				return directoryPath;
			}
            set 
			{
				directoryPath = value;
				PropertyChanged(this, new PropertyChangedEventArgs("DirectoryPath"));
				load_directory();
			}
		}
        private string directoryPathEdit { get; set; } = @"C:/Users/Kurek/Videoss";
        public string DirectoryPathEdit
        {
            get
            {
                return directoryPathEdit;
            }
            set
            {
				if (value.EndsWith(Environment.NewLine))
				{
					DirectoryPath = directoryPathEdit;
                    return;
				}
				directoryPathEdit = value;
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        private FileInfo[] files = new FileInfo[0];
        public FileInfo[] Files 
		{
			get
			{
				return files;
			}
			set
			{
				files = value;
			//	PropertyChanged(this, new PropertyChangedEventArgs("Files"));
			}
		}

		private int fileIndex;
		public int FileIndex {
            get {
				return fileIndex;
			}
            set 
			{
				if(files!=null)
				if (value >= 0 && value < files.Length)
				{
					fileIndex = value;

					FilePath = files[value].FullName;
					load_video();
					LoadVideoTimmer();
				}
			} 
		}

		private string selectedFile = String.Empty;
		public string SelectedFile
		{
			get
			{
				return selectedFile;
			}
			set
			{
				selectedFile = value;
				FilePath = selectedFile;
			}
		}
		public MainWindow(string file)
		{
			//icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
			//var xd=this.FindResource("videosDir");
			//Directory.EnumerateFiles("E:\\medalek");
			initializePlayer();
			directoryPath = file.Remove(file.LastIndexOf('\\'));//thorow
			load_directory();//
			filePath = file;
			for (int i = 0; i < files.Length; i++)	
			{
				if (files[i].FullName.Substring(0, files[i].FullName.LastIndexOf(".")) ==file) 
				{
					FileIndex = i;
					break;
				}
			}
			
			load_video();
		
		}
		public MainWindow()
		{
			initializePlayer();
			load_directory();
			try { 
				filePath = files[0].FullName;
			}
			catch (Exception e)
			{
				;
            }

            load_video();
		}

		private void load_directory()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
			string[] extensions = new[] { ".mp4", ".mkv"};
			try
			{
				Files = directoryInfo.EnumerateFiles().Where(f => extensions.Contains(f.Extension.ToLower())).ToArray();
				if (Files.Length == 0)
					directoryInfo.GetFiles();
				Array.Sort(Files, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(y.CreationTime, x.CreationTime));
			}catch(Exception ex)
			{
				;
			}
		}

		private void initializePlayer()
		{
			InitializeComponent();

			DataContext = this;



			setUpSpeeds();

			DoubleClickTimer.Tick += (s, e) => DoubleClickTimer.Stop();
			timer.Interval = TimeSpan.FromMilliseconds(34);
			timer.Tick += Tick;
			timer.Start();

			VolumeSliderTimer.Interval = TimeSpan.FromSeconds(18);
			VolumeSliderTimer.Tick += (s, e) => VolumeSubmitted(s, e);
			VolumeSliderTimer.Start();

		}
		private void LoadVideoTimmer()
		{
			if (player.NaturalDuration.HasTimeSpan)
			{	
				slider.Maximum = player.NaturalDuration.TimeSpan.TotalMilliseconds;
				TimeSlider = 0;
				Status.Content = String.Format("{0} / {1}", player.Position.ToString(@"mm\:ss"), player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
			}
		}

		
		private void player_MediaOpened(object sender, RoutedEventArgs e)
		{
			LoadVideoTimmer();
		}

		private void VolumeSubmitted(object s, EventArgs e)
        {
			volumeSlider.Visibility = Visibility.Hidden;
			VolumeSliderTimer.Stop();
        }

        void Tick(object sender, EventArgs e)
		{
			if (update)
			{
				player.Position = new TimeSpan(0, 0, 0, 0, (int)TimeSlider);
				update = false;
			}
			if (playing && player.Source != null)
			{

				if (player.NaturalDuration.HasTimeSpan)
				{
					Status.Content = String.Format("{0} / {1}", player.Position.ToString(@"mm\:ss"), player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));//to potem
					//if(!update)
					Time = player.Position.TotalMilliseconds;

				}
			}


		}

		private void setUpSpeeds()
		{
			slider.SmallChange = player.SpeedRatio * 1500;
			slider.LargeChange = player.SpeedRatio * 15000;
		}

		public string Speed => player.SpeedRatio.ToString() + "x";
		private void load_video()
		{
			playing = false;
			try { 
				player.Source = new Uri(FilePath);
            }catch (Exception ex)
			{
				return;
			}
            lblSpeed.Content = Speed;
			if (!player.NaturalDuration.HasTimeSpan)
			{
				slider.Visibility = Visibility.Hidden;
				player.Position = new TimeSpan(0, 0, 0);
			}
				player.Play();
			player.Pause();
		}

		private void btnPlay_Click(object sender, RoutedEventArgs e)
		{
			playPause();
		}

		private void playPause()
		{
			if (!playing)
			{
				Play();
				return;
			}
			Pause();
		}


		private void Play()
		{
			playing = true;
			TimeSlider=player.Position.TotalMilliseconds;//
			player.Play();
		}
		private void Pause()
		{
			playing = false;
			player.Pause();
		}

		private void btnSpeed_Click(object sender, RoutedEventArgs e)
		{
			SpeedUp();
		}

		private void SpeedDown()
		{
			if (speedIndex > 0)
			{
				player.SpeedRatio = speeds[--speedIndex];
				lblSpeed.Content = Speed;
				setUpSpeeds();
			}
		}

		private void btnSlow_Click(object sender, RoutedEventArgs e)
		{
			SpeedDown();
		}

		private void SpeedUp()
		{
			if (speedIndex < speeds.Length - 1)
			{
				player.SpeedRatio = speeds[++speedIndex];
				lblSpeed.Content = Speed;
				setUpSpeeds();
			}
		}




		private void mePlayer_Drop(object sender, DragEventArgs e)
        {
			//videosList.UnselectAll();
			FilePath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
		}

     

        private void mePlayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (hud)
			{
				HideHud();
			}
			else
			{
				ShowHud();
			}
			if (!DoubleClickTimer.IsEnabled)
			{
				DoubleClickTimer.Start();
			}
			else
			{
				if (!fullscreen)
				{
					this.WindowStyle = WindowStyle.None;
					this.WindowState = WindowState.Maximized;
				}
				else
				{
					this.WindowStyle = WindowStyle.SingleBorderWindow;
					this.WindowState = WindowState.Normal;
				}
				DoubleClickTimer.Stop();
				fullscreen = !fullscreen;
			}
		}

		[DllImport("user32.dll")]
		private static extern uint GetDoubleClickTime();

        private void mePlayer_MouseMove(object sender, MouseEventArgs e)
        {
			ShowHud();
		}

        private void ShowHud()
        {
			soundbtn.Visibility = Visibility.Visible;
			slider.Visibility = Visibility.Visible;
			Status.Visibility = Visibility.Visible;
			panel.Visibility = Visibility.Visible;
			Cursor = Cursors.Arrow;
			hud = true;
		}
		private void HideHud() 
		{
			volumeSlider.Visibility = Visibility.Hidden;
			soundbtn.Visibility = Visibility.Hidden;
			slider.Visibility = Visibility.Hidden;
			Status.Visibility = Visibility.Hidden;
			panel.Visibility = Visibility.Hidden;
			Cursor = Cursors.None;
			hud = false;
		}

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
				case Key.Escape:
					this.Close();
					return;
				case Key.Left:
				case Key.Back:
					TimeSlider -= slider.SmallChange;
					return;
				case Key.Right:
					TimeSlider += slider.SmallChange;
					return;
				case Key.J:
				case Key.A:
					TimeSlider -= slider.LargeChange;
					return;
                case Key.L:
				case Key.D:
					TimeSlider += slider.LargeChange;
					return;
				case Key.Space:
				case Key.K:
				case Key.MediaPlayPause:
					playPause();
					return;
				case Key.Up:
				case Key.VolumeUp:
					Volume+=5;
					return;
				case Key.Down:
				case Key.VolumeDown:
					Volume-=5;
					return;
				case Key.Add:
					SpeedUp();
					return;
				case Key.Subtract:
					SpeedDown();
					return;
				case Key.Play:
					Play();
					return;
				case Key.F8:
					WindowState = WindowState.Minimized;
					return;
				case Key.MediaStop:
					Pause();
					return;
				case Key.MediaNextTrack:
					fileIndex++;
					return;
				case Key.MediaPreviousTrack:
					fileIndex--;
					return;
			}
            return;

		}


		private void soundbtn_Click(object sender, RoutedEventArgs e)
		{
			if (volumeSlider.Visibility == Visibility.Hidden)
			{
				volumeSlider.Visibility = Visibility.Visible;
				return;
			}
			volumeSlider.Visibility = Visibility.Hidden;
			player.Play();
			player.Pause();
		
		}

		private void ShowVolumeSlider() 
		{
			DoubleAnimation showAnim = new DoubleAnimation();
			showAnim.Duration = TimeSpan.FromSeconds(0.3);
			showAnim.EasingFunction = new PowerEase()
			{
				EasingMode = EasingMode.EaseIn
			};
			showAnim.From = (double?)Visibility.Hidden;
			showAnim.To = (double?)Visibility.Visible;

			volumeSlider.BeginAnimation(VisibilityProperty, showAnim);
		}

		private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
			if (e.Delta > 0)
			{
				Volume+=2;
				return;
			}
			Volume-=2;
        }

        private void btnNextVid_Click(object sender, RoutedEventArgs e)
        {
			FileIndex++;
        }

        private void btnPrevVid_Click(object sender, RoutedEventArgs e)
        {
			FileIndex--;
		}

        private void btnVideosList_Click(object sender, RoutedEventArgs e)
        {
			if (videosListLayout.Visibility == Visibility.Hidden)
			{
				videosListLayout.Visibility = Visibility.Visible;//Grid.RowSpan
				player.SetValue(Grid.ColumnSpanProperty, 2);
				return;
			}
			player.SetValue(Grid.ColumnSpanProperty, 4);
			videosListLayout.Visibility = Visibility.Hidden;
        }

        private void btnNextFrame_Click(object sender, RoutedEventArgs e)
        {
			//TODO

			//DoubleAnimation showAnim = new DoubleAnimation();
			//showAnim.Duration = TimeSpan.FromSeconds(0.3);
			//showAnim.EasingFunction = new PowerEase() { 
			//	EasingMode = EasingMode.EaseIn
			//};
			//showAnim.From = videosListLayout.DesiredSize.Width;
			//showAnim.To = 0;
			//videosListLayout.BeginAnimation( WidthProperty, showAnim);
		}

        private void btnPrevFrame_Click(object sender, RoutedEventArgs e)
        {
			//TODO

			//(testing animations (not related)
			//DoubleAnimation showAnim = new DoubleAnimation();
			//showAnim.Duration = TimeSpan.FromSeconds(0.3);
			//showAnim.EasingFunction = new PowerEase()
			//{
			//	EasingMode = EasingMode.EaseIn
			//};
			//showAnim.From = 0;
			//showAnim.To = grid.DesiredSize.Width;
			//videosList.BeginAnimation( LeftProperty, showAnim);
		}

		private void DirButton_Click(object sender, RoutedEventArgs e)
		{
			FolderPicker folderPicker = new FolderPicker();

			if(folderPicker.ShowDialog()==true)
            {
				DirectoryPath = folderPicker.ResultPath;
            }
		}
	}
}
