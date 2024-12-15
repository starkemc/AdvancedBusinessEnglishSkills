using AdvancedBusinessEnglishSkills.Data;
using CommunityToolkit.Maui.Views;
using DevExpress.Data.Helpers;
using System.Collections.ObjectModel;

namespace AdvancedBusinessEnglishSkills;

public partial class Practice : ContentView
{
    DBContext _dbContext = new();
    private int _menuId;
    public ObservableCollection<Models.PracticeDetail> Data { get; set; } = new();
    private List<Models.Practice> _practice = new();

    MediaState _mediaState;

    public Practice(int menuId)
	{
		InitializeComponent();

        _menuId = menuId;

        _mediaState = MediaState.Stopped;

        mediaPlayer.PropertyChanged += MediaPlayer_PropertyChanged;
    }

    public async Task LoadDataAsync()
    {
        _practice = await _dbContext.Practice_GetByMenuId(_menuId);

        if (!_practice.Any())
            return;

        foreach (var item in _practice)
        {
            var details = await _dbContext.PracticeDetail_GetByPracticeId(item.Id);
            item.Items.AddRange(details);
        }

        var radioName1 = this.FindByName("RadioName1") as RadioButton;
        radioName1.Content = _practice[0].Name;
        radioName1.IsChecked = true;

        var radioName2 = this.FindByName("RadioName2") as RadioButton;
        radioName2.Content = _practice[1].Name;

        Data.Clear();
        _practice[0].Items.ForEach(item => { Data.Add(item); });
        collectionView.ItemsSource = Data;

        //set audiofile
        mediaPlayer.Source = MediaSource.FromResource(_practice[0].Audio);       

    }

    private void RadioName1_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var button = (RadioButton)sender;

        if (button.IsChecked)
        {
            var name = button.Content.ToString();

            //data to bind
            var practice = _practice.FirstOrDefault(d => d.Name == name);

            Data.Clear();
            practice.Items.ForEach(item => { Data.Add(item); });
            collectionView.ItemsSource = Data;

            //set audiofile
            mediaPlayer.Source = MediaSource.FromResource(practice.Audio);
        }
    }

    #region Media Player

    public void StopPlayer()
    {
        if (_mediaState == MediaState.Playing)
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

public class PracticeRowStyle : DataTemplateSelector
{
    public DataTemplate EvenRowTemplate { get; set; }
    public DataTemplate OddRowTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var boundPractice = item as Models.PracticeDetail;

        if (boundPractice.Enabled == 1)
            return OddRowTemplate;

        return EvenRowTemplate;

        //return Convert.ToInt32(boundPractice.id) % 2 == 0 ? EvenRowTemplate : OddRowTemplate;
    }
}