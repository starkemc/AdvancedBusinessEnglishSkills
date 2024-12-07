using AdvancedBusinessEnglishSkills.Data;
using CommunityToolkit.Maui.Views;


namespace AdvancedBusinessEnglishSkills;

public partial class Listen : ContentView
{
    DBContext _database;

    public Listen()
	{
		InitializeComponent();

        _database = new DBContext();

        mediaPlayer.PropertyChanged += MediaPlayer_PropertyChanged;
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        if (this.Parent != null) // The ContentView is now in the visual tree
        {
            await LoadDataAsync();
        }
    }

    private async Task LoadDataAsync()
    {
        //load the data from sqlite
        var items = await _database.Listen_GetByMenuId(MenuId);
        var audioFile = await _database.Audio_GetListenByMenuId(MenuId);

        //set audiofile
        mediaPlayer.Source = MediaSource.FromResource(audioFile.Name);

        //set collectionView
        collectionView.ItemsSource = items;
    }

    public int MenuId { get; set; }

    #region Media Player

    private void MediaPlayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            slider.Maximum = mediaPlayer.Duration.TotalSeconds;

            var timespan = TimeSpan.FromSeconds(mediaPlayer.Duration.TotalSeconds);
            AudioLength.Text = FormatTime(timespan.Minutes, timespan.Seconds);
        }
    }

    private void MediaElement_PositionChanged(object sender, CommunityToolkit.Maui.Core.Primitives.MediaPositionChangedEventArgs e)
    {
        slider.Value = e.Position.TotalSeconds;

        AudioTicker.Text = FormatTime(e.Position.Minutes, e.Position.Seconds);
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        mediaPlayer.Play();

        Play.IsVisible = false;
        Pause.IsVisible = true;

    }

    private void Pause_Clicked(object sender, EventArgs e)
    {
        mediaPlayer.Pause();

        Play.IsVisible = true;
        Pause.IsVisible = false;
    }

    private void slider_DragStarted(object sender, EventArgs e)
    {
        mediaPlayer.Pause();
    }

    private void slider_DragCompleted(object sender, EventArgs e)
    {
        var newValue = ((Slider)sender).Value;
        mediaPlayer.SeekTo(TimeSpan.FromSeconds(newValue));

        var timespan = TimeSpan.FromSeconds(newValue);
        AudioTicker.Text = FormatTime(timespan.Minutes, timespan.Seconds);

        mediaPlayer.Play();
    }

    private string FormatTime(int minutes, int seconds)
    {
        return $"{minutes:D2}:{seconds:D2}";

        //string minute = string.Empty;
        //string second = string.Empty;

        //if (minutes <= 9)
        //    minute = $"0{minutes}";
        //else
        //    minute = minutes.ToString();

        //if (seconds <= 9)
        //    second = $"0{seconds}";
        //else
        //    second = seconds.ToString();

        //return $"{minute}:{second}";

    }

    #endregion

}