﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Linq;
using Shintenbou.Rest;

namespace Shintenbou.Pages
{
    public class AnimePage : AnimePageBase
    {
        public AnimePage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.Grid = this.Find<Grid>("AnimeGrid");
            this.TextBox = this.Find<TextBox>("AnimeSearch");
            this.Button = this.Find<Button>("AnimeSearchButton");
            this.TextBox.KeyDown += OnKeyDown;
            this.Button.Click += OnQuerySubmit;
        }

        private async void OnQuerySubmit(object sender, RoutedEventArgs e)
        {
            await ClearImagesAsync();
            var animes = await Anilist.GetAnimeByNameAsync(this.TextBox.Text);
            if(animes != null) await DisplayAnimesAsync(animes.ToList());
        }

        private async void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                await ClearImagesAsync();
                var animes = await Anilist.GetAnimeByNameAsync(this.TextBox.Text);
                if(animes != null) await DisplayAnimesAsync(animes.ToList());
            }
        }
    }
}
