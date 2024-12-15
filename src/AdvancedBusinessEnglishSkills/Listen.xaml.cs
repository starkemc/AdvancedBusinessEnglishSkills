using AdvancedBusinessEnglishSkills.Data;
using CommunityToolkit.Maui.Views;


namespace AdvancedBusinessEnglishSkills;

public partial class Listen : ContentView
{
    DBContext _database = new();
    MediaState _mediaState;

    public Listen(int menuId) 
    {
        MenuId = menuId;

        InitializeComponent();

        mediaPlayer.PropertyChanged += MediaPlayer_PropertyChanged;

        _mediaState = MediaState.Stopped;

       // OnParentSet();
    }

    public Listen()
	{
		InitializeComponent();

        mediaPlayer.PropertyChanged += MediaPlayer_PropertyChanged;
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        //if (this.Parent != null) // The ContentView is now in the visual tree
        //{
        //    await LoadDataAsync();
        //}
    }

    public async Task LoadDataAsync()
    {
        //load the data from sqlite
        var items = await _database.Listen_GetByMenuId(MenuId);
        var audioFile = await _database.Audio_GetListenByMenuId(MenuId);

        if (audioFile != null && items != null) 
        {
            //set audiofile
            mediaPlayer.Source = MediaSource.FromResource(audioFile.Name);

            //set collectionView
            collectionView.ItemsSource = items;
        }
    }

    public int MenuId { get; set; }

    #region Media Player

    public void StopPlayer()
    {
        if(_mediaState == MediaState.Playing)
        {
            mediaPlayer.Pause();
            Play.IsVisible = true;
            Pause.IsVisible = false;
        }            
    }

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

        _mediaState = MediaState.Playing;
    }

    private void Pause_Clicked(object sender, EventArgs e)
    {
        mediaPlayer.Pause();

        Play.IsVisible = true;
        Pause.IsVisible = false;

        _mediaState = MediaState.Paused;
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
    }

    private void mediaPlayer_MediaEnded(object sender, EventArgs e)
    {
        _mediaState = MediaState.Stopped;
    }

    #endregion

}

public enum MediaState
{
    Playing,
    Paused,
    Stopped
}

public class ListenRowStyle : DataTemplateSelector
{
    public DataTemplate EvenRowTemplate { get; set; }
    public DataTemplate OddRowTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var listen = item as Models.Listen;               

        return Convert.ToInt32(listen.Id) % 2 == 0 ? EvenRowTemplate : OddRowTemplate;
    }
}