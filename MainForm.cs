using Guna.UI2.WinForms;
using Keyboard_Typing.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters.Entities;

namespace Keyboard_Typing
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeSounds();
        }
        enum enOptions
        {
            None, Settings, FillColor, Audio, Keyboard, Refresh, Play, Pause, Menu
        }
        enum enKeyCapsActiveColors
        {
            CurrentChar, RightChar, WrongChar
        }
        enum enKeyboardThemes
        {
            Standard, Colorful, RGB
        }
        enum enCurrentOptionButtonColor
        {
            ColorSelected, DefaultAfterLeaveColor, MouseEnterColor
        }
        enum enCurrentFormBackground
        {
            Default, Pinky, Birds, Black
        }
        struct stRGBVariables
        {
            public Random random;
            public int currentRed;
            public int currentGreen;
            public int currentBlue;
            public int steps;
        }
        struct stCurrentThemeColors
        {
            public Color PBFont;
            public Color PBBack;
            public Color PBRightCharFore;
            public Color PBRightCharBack;
            public Color PBWrongCharFore;
            public Color PBWrongCharBack;
            public Color PBCorrectionCharFore;
            public Color PBCorrectionCharBack;
            public Color KLFore;
            public Color KLBack;
            public Color SelectedOption;
            public Color OptMouseEnterBackIMG;
            public Color OptDefaultORMouseLeaveBackIMG;
            public Color AllPanelsFore;
            public Color AllPanelsBack;
            public Color LblSizesFore;
        }
        struct stSounds
        {
            public SoundPlayer RighCharOrBackSpace;
            public SoundPlayer WrongChar;
            public SoundPlayer Space;
        }
        struct stTypingVariables
        {
            public DateTime startTime;
            public DateTime lastKeystrokeTime;
            public int totalKeystrokes;
            public int correctKeystrokes;
            public int incorrectKeystrokes;
        }
        struct stIllusionVariables
        {
            public float RatingStartargetValue;
            public short SpeedCircletargetValue;
            public short DurationCircletargetValue;
            public short AccuracyCircletargetValue;
        }
        struct stStatus
        {
            public stRGBVariables RGBVariables;
            public stCurrentThemeColors CurrentThemeColors;
            public stTypingVariables TypingResults;
            public stIllusionVariables IllusionVariables;
            public enOptions ChoosedOption;
            public enKeyboardThemes CurrentKeyboardTheme;
            public enCurrentOptionButtonColor CurrentOptionButtonColor;
            public enOptions CurrentOpenedOptionButton;
            public enCurrentFormBackground CurrentFormBackground;
            public Button LastChoosed;
            public short charPosition;
            public stSounds Sound;
            public byte SeqWrongs;
            public Color LastForeColor;
            public Color LastBackColor;
            public List<short> RightCharPositions;
            public List<short> WrongCharPositions;
            public List<short> CorrectedCharPositions;
        }
        stStatus APPStatus;
        private void SetAllFormControlsTheme(Color PBFont, Color PBBack, Color PBRightCharFore, Color PBRightCharBack, Color PBWrongCharFore,
                                             Color PBWrongCharBack, Color PBCorrectionCharFore, Color PBCorrectionCharBack, Color KLFore,
                                             Color KLBack, Color SelectedOption, Color OptMouseEnterBackIMG, Color OptDefaultORMouseLeaveBackIMG, Color AllPanelsFore,
                                             Color AllPanelsBack, Color LBLSizesFore)
        {
            APPStatus.CurrentThemeColors.PBFont = PBFont;
            APPStatus.CurrentThemeColors.PBBack = PBBack;
            APPStatus.CurrentThemeColors.PBRightCharFore = PBRightCharFore;
            APPStatus.CurrentThemeColors.PBRightCharBack = PBRightCharBack;
            APPStatus.CurrentThemeColors.PBWrongCharFore = PBWrongCharFore;
            APPStatus.CurrentThemeColors.PBWrongCharBack = PBWrongCharBack;
            APPStatus.CurrentThemeColors.PBCorrectionCharFore = PBCorrectionCharFore;
            APPStatus.CurrentThemeColors.PBCorrectionCharBack = PBCorrectionCharBack;
            APPStatus.CurrentThemeColors.KLFore = KLFore;
            APPStatus.CurrentThemeColors.KLBack = KLBack;
            APPStatus.CurrentThemeColors.SelectedOption = SelectedOption;
            APPStatus.CurrentThemeColors.OptMouseEnterBackIMG = OptMouseEnterBackIMG;
            APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG = OptDefaultORMouseLeaveBackIMG;
            APPStatus.CurrentThemeColors.AllPanelsFore = AllPanelsFore;
            APPStatus.CurrentThemeColors.AllPanelsBack = AllPanelsBack;
            APPStatus.CurrentThemeColors.LblSizesFore = LBLSizesFore;
        }
        private void ChangeAllFormTheme()
        {
            switch (APPStatus.CurrentFormBackground)
            {
                case enCurrentFormBackground.Default:
                    {
                        SetAllFormControlsTheme(Color.Gray, Color.White,
                            Color.Green, Color.LightGreen, // right fore and back
                            Color.DarkRed, Color.LightPink, // wrong fore and back
                            Color.DarkGreen, Color.Yellow, // correction fore and back
                            Color.Gray, Color.Transparent, Color.Blue, Color.Black,
                            Color.Gray, Color.Black, Color.WhiteSmoke, Color.Gray);
                        break;
                    }
                case enCurrentFormBackground.Pinky:
                    {
                        SetAllFormControlsTheme(Color.Gray, Color.FromArgb(245, 231, 237),
                            Color.Black, Color.White, // right fore and back
                            Color.DarkRed, Color.LightPink, // wrong fore and back
                            Color.DarkGreen, Color.Yellow, // correction fore and back
                            Color.Gray, Color.Transparent, Color.Blue, Color.Black,
                            Color.Gray, Color.Black, Color.FromArgb(250, 240, 242), Color.Gray);
                        break;
                    }
                case enCurrentFormBackground.Birds:
                    {
                        SetAllFormControlsTheme(Color.Gray, Color.FromArgb(221, 224, 241),
                            Color.SlateBlue, Color.White, // right fore and back
                            Color.DarkRed, Color.LightPink, // wrong fore and back
                            Color.DarkGreen, Color.Yellow, // correction fore and back
                            Color.Gray, Color.Transparent, Color.Blue, Color.Black,
                            Color.Gray, Color.Black, Color.FromArgb(233, 235, 246), Color.Gray);
                        break;
                    }
                default: // this the black one
                    {
                        SetAllFormControlsTheme(Color.Teal, Color.FromArgb(13, 17, 16),
                            Color.LightGreen, Color.DarkGreen, // right fore and back
                            Color.DarkRed, Color.LightPink, // wrong fore and back
                            Color.Brown, Color.Yellow, // correction fore and back
                            Color.Gray, Color.Transparent, Color.Blue, Color.White,
                            Color.Orange, Color.FromArgb(197, 148, 56), Color.FromArgb(27, 39, 37), Color.FromArgb(197, 148, 56));
                        break;
                    }
            }
            ChangeAllControlsColors();
        }
        private void btnChangePhoto_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = ((Button)sender).BackgroundImage;
            if (sender as Button == btnDefaultPhoto)
            {
                APPStatus.CurrentFormBackground = enCurrentFormBackground.Default;
            }
            else if (sender as Button == btnFirstPhoto)
            {
                APPStatus.CurrentFormBackground = enCurrentFormBackground.Pinky;
            }
            else if (sender as Button == btnSecondPhoto)
            {
                APPStatus.CurrentFormBackground = enCurrentFormBackground.Birds;
            }
            else
            {
                APPStatus.CurrentFormBackground = enCurrentFormBackground.Black;
            }
            ChangeAllFormTheme();
            ParagraphBox_TextChanged(ParagraphBox.Text[APPStatus.charPosition], false);
        }
        private void StoreParagraphWrittenCharsColors()
        {
            APPStatus.RightCharPositions = new List<short>();
            APPStatus.WrongCharPositions = new List<short>();
            APPStatus.CorrectedCharPositions = new List<short>();
            if (APPStatus.charPosition > 0)
            {
                for (short i = 0; i < APPStatus.charPosition; i++)
                {
                    ParagraphBox.Select(i, 1);
                    if (ParagraphBox.SelectionBackColor == Color.LightGreen || ParagraphBox.SelectionBackColor == Color.White ||
                        ParagraphBox.SelectionBackColor == Color.DarkGreen)
                    { // it was Right char
                        APPStatus.RightCharPositions.Add(i); // now we stored a right char position
                    }
                    else if (ParagraphBox.SelectionBackColor == Color.LightPink)
                    { // it was Wrong char
                        APPStatus.WrongCharPositions.Add(i); // now we stored a wrong char position
                    }
                    else
                    { // it was Corrected char
                        APPStatus.CorrectedCharPositions.Add(i); // now we stored a corrected char position
                    }
                }
            }
            ParagraphBox.Select(APPStatus.charPosition, 1);
        }
        private void SetParagraphWrittenCharsColors()
        {
            if (APPStatus.charPosition > 0)
            {
                foreach (short element in APPStatus.RightCharPositions)
                {
                    ParagraphBox.Select(element, 1);
                    ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBRightCharFore;
                    ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBRightCharBack;
                }
                foreach (short element in APPStatus.WrongCharPositions)
                {
                    ParagraphBox.Select(element, 1);
                    ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBWrongCharFore; ;
                    ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBWrongCharBack;
                }
                foreach (short element in APPStatus.CorrectedCharPositions)
                {
                    ParagraphBox.Select(element, 1);
                    ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBCorrectionCharFore; ;
                    ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBCorrectionCharBack;
                }
                for (int i = APPStatus.charPosition; i < ParagraphBox.Text.Length; i++)
                {
                    ParagraphBox.Select(i, 1);
                    ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBFont;
                    ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBBack;
                }
            }
            ParagraphBox.Select(APPStatus.charPosition, 1);
        }
        private void ResetParagraphBoxColors()
        {
            StoreParagraphWrittenCharsColors();
            ParagraphBox.SelectAll();
            ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBFont;
            ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBBack;
            ParagraphBox.DeselectAll();
            ParagraphBox.ForeColor = APPStatus.CurrentThemeColors.PBFont;
            ParagraphBox.BackColor = APPStatus.CurrentThemeColors.PBBack;
            SetParagraphWrittenCharsColors();
        }
        private void ResetKeyCapsColors()
        {
            if (APPStatus.CurrentKeyboardTheme != enKeyboardThemes.Colorful)
            {
                foreach (Label LBL in pnlAllKeyBoard.Controls)
                {
                    LBL.ForeColor = APPStatus.CurrentThemeColors.KLFore;
                    LBL.BackColor = APPStatus.CurrentThemeColors.KLBack;
                }
                lblK55.Image = Resources.Windows_11;
            }
            else
            {
                Change_Key_Cap_Theme_To_ColorFul();
            }
        }
        private void ResetOptionsButtons()
        {
            Button[] buttons = { btnSettings, btnFillColor, btnAudio, btnKeyboard, btnRefresh, btnPausePlay, btnMenu }; // ممكن هنا أعمل استثناء للبوز
            foreach (Button btn in buttons)
            {
                if (APPStatus.LastChoosed == btn) ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.SelectedOption);
                else ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
            }
        }
        private void ResetPanelsOptionsColors()
        {
            Panel[] panels = { PanelOptions, PanelColorFillDetails, PanelAudioDetails, pnlLessonsList, PanelKeyboardDetails, PanelSettingsDetails };
            foreach (Panel panel in panels)
            {
                panel.ForeColor = APPStatus.CurrentThemeColors.AllPanelsFore;
                panel.BackColor = APPStatus.CurrentThemeColors.AllPanelsBack;
            }
        }
        private void ResetLabelsColors()
        {
            Label[] labels = {lblFontSize, lblBackground,lblSparklingStars, lblTastyTreats, lblFirendshipPower,
                lblPracticeMakesPerfect, lblCleverCrows, lblKeyAudio,lblShowFastRate, lblBlockOnFault, lblRGBSpeed,
                lblShowKeyboard, lblKeyboardTheme};
            foreach (Label lbl in labels)
            {
                lbl.BackColor = APPStatus.CurrentThemeColors.AllPanelsBack;
                lbl.ForeColor = APPStatus.CurrentThemeColors.AllPanelsFore;
            }
        }
        private void ResetGunaBtnsColors()
        {
            Guna2Button[] btnSizes = { btnSmallFontSize, btnMediumFontSize, btnLargeFontSize };
            foreach (Guna2Button btn in btnSizes)
            {
                btn.ForeColor = APPStatus.CurrentThemeColors.LblSizesFore;
                btn.BackColor = APPStatus.CurrentThemeColors.AllPanelsBack;
            }
        }
        private void ChangeAllControlsColors()
        {
            ResetParagraphBoxColors();
            ResetKeyCapsColors();
            ResetOptionsButtons();
            ResetPanelsOptionsColors();
            ResetLabelsColors();
            ResetGunaBtnsColors();
        }
        private void Enable_Disable_Panel(Panel pnl, bool ISEnabled)
        {
            pnl.Enabled = ISEnabled;
            pnl.Visible = ISEnabled;
        }
        private void ChangeOptionsButtonImage(Button btn, Color Clr)
        {
            switch (btn.Tag.ToString())
            {
                case "Settings":
                    {
                        if (Clr == Color.Blue) btn.BackgroundImage = Resources.BlueSettingsBtn;
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GraySettingsBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangeSettingsBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhiteSettingsBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Settings;
                        break;
                    }
                case "FillColor":
                    {
                        if (Clr == Color.Blue) btn.BackgroundImage = Resources.BlueFillColorBtn;
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GryFill_ColorBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangeFillColorBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhiteFillColorBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Fill_Color;
                        break;
                    }
                case "Audio":
                    {
                        if (Clr == Color.Blue) btn.BackgroundImage = Resources.BlueAudioBtn;
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GryAudioBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangeAudioBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhiteAudioBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Audio;
                        break;
                    }
                case "Keyboard":
                    {
                        if (Clr == Color.Blue) btn.BackgroundImage = Resources.BlueKeyboardBtn;
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GryKeyboardBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangeKeyboardBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhiteKeyboardBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Keyboard;
                        break;
                    }
                case "Menu":
                    {
                        if (Clr == Color.Blue) btn.BackgroundImage = Resources.BlueMenuBtn;
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GrayMenuBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangedMenuBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhiteMenuBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Menu;
                        break;
                    }
                case "Refresh":
                    {
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GrayResetBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangeResetBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhiteResetBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Reset;
                        break;
                    }
                case "Play":
                    {
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GrayPlayBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangePlayBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhitePlayBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Play;
                        break;
                    }
                case "Pause":
                    {
                        if (Clr == Color.Gray) btn.BackgroundImage = Resources.GrayPauseBtn;
                        if (Clr == Color.Orange) btn.BackgroundImage = Resources.OrangePauseBtn;
                        if (Clr == Color.White) btn.BackgroundImage = Resources.WhitePauseBtn;
                        if (Clr == Color.Black) btn.BackgroundImage = Resources.Pause;
                        break;
                    }
            }
        }
        private void ManagePanelsTraffic(bool Enabled = true)
        {
            switch (APPStatus.ChoosedOption)
            {
                case enOptions.None:
                    {
                        break;
                    }
                case enOptions.Settings:
                    {
                        Enable_Disable_Panel(PanelSettingsDetails, Enabled);
                        if (!Enabled)
                            ChangeOptionsButtonImage(btnSettings, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
                case enOptions.FillColor:
                    {
                        Enable_Disable_Panel(PanelColorFillDetails, Enabled);
                        if (!Enabled)
                            ChangeOptionsButtonImage(btnFillColor, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
                case enOptions.Audio:
                    {
                        Enable_Disable_Panel(PanelAudioDetails, Enabled);
                        if (!Enabled)
                            ChangeOptionsButtonImage(btnAudio, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
                case enOptions.Keyboard:
                    {
                        Enable_Disable_Panel(PanelKeyboardDetails, Enabled);
                        if (!Enabled)
                            ChangeOptionsButtonImage(btnKeyboard, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
                case enOptions.Menu:
                    {
                        Enable_Disable_Panel(pnlLessonsList, Enabled);
                        if (!Enabled)
                            ChangeOptionsButtonImage(btnMenu, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
                case enOptions.Pause:
                    {
                        btnPausePlay.Tag = "Play";
                        ChangeOptionsButtonImage(btnPausePlay, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
                case enOptions.Play:
                    {
                        btnPausePlay.Tag = "Pause";
                        ChangeOptionsButtonImage(btnPausePlay, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                        break;
                    }
            }
        }
        private void ManageChoosedPanel(Button btn, enOptions Option)
        {
            if (APPStatus.ChoosedOption != Option)
            {
                ManagePanelsTraffic(false); // disable last opened panel
                APPStatus.ChoosedOption = Option;
                ManagePanelsTraffic(true); // Enable New Panel
                ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.SelectedOption);
                APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.ColorSelected; // the button is currently selected
                APPStatus.CurrentOpenedOptionButton = Option;
                APPStatus.LastChoosed = btn;
            }
            else // if the last button was the current Already, we will just colse the panel
            {
                ManagePanelsTraffic(false);
                APPStatus.ChoosedOption = enOptions.None;
                ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                APPStatus.CurrentOpenedOptionButton = enOptions.None;
                APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.DefaultAfterLeaveColor; // default color
            }
        }
        private void btnOptions_Click(object sender, EventArgs e) // سيتم اختصارها لا محال
        {
            Button btn = (Button)sender;
            switch (btn.Tag.ToString())
            {
                case "Settings":
                    {
                        ManageChoosedPanel(btn, enOptions.Settings);
                        if (APPStatus.ChoosedOption == enOptions.Settings) PanelSettingsDetails.Focus();
                        else ParagraphBox.Focus();
                        break;
                    }
                case "FillColor":
                    {
                        ManageChoosedPanel(btn, enOptions.FillColor);
                        if (APPStatus.ChoosedOption == enOptions.FillColor) PanelSettingsDetails.Focus();
                        else ParagraphBox.Focus();
                        break;
                    }
                case "Audio":
                    {
                        ManageChoosedPanel(btn, enOptions.Audio);
                        if (APPStatus.ChoosedOption == enOptions.Audio) PanelSettingsDetails.Focus();
                        else ParagraphBox.Focus();
                        break;
                    }
                case "Keyboard":
                    {
                        ManageChoosedPanel(btn, enOptions.Keyboard);
                        if (APPStatus.ChoosedOption == enOptions.Keyboard) PanelSettingsDetails.Focus();
                        else ParagraphBox.Focus();
                        break;
                    }
                case "Menu":
                    {
                        ManageChoosedPanel(btn, enOptions.Menu);
                        if (APPStatus.ChoosedOption == enOptions.Menu) PanelSettingsDetails.Focus();
                        else ParagraphBox.Focus();
                        break;
                    }
                case "Refresh":
                    {
                        APPStatus.LastChoosed = null;
                        REFRESH();
                        ParagraphBox.Focus();
                        break;
                    }
                case "Pause":
                    {
                        ManagePanelsTraffic(false); // disable last opened panel
                        APPStatus.ChoosedOption = enOptions.Pause;
                        ManagePanelsTraffic(true);
                        APPStatus.CurrentOpenedOptionButton = enOptions.None;
                        APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.DefaultAfterLeaveColor;
                        Timer.Stop();
                        if (APPStatus.CurrentKeyboardTheme == enKeyboardThemes.RGB) RGBTimer.Stop();
                        ParagraphBox.Focus();
                        break;
                    }
                case "Play":
                    {
                        ManagePanelsTraffic(false); // disable last opened panel
                        APPStatus.ChoosedOption = enOptions.Play;
                        ManagePanelsTraffic(true);
                        APPStatus.CurrentOpenedOptionButton = enOptions.None;
                        APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.DefaultAfterLeaveColor;
                        Timer.Start();
                        if (APPStatus.CurrentKeyboardTheme == enKeyboardThemes.RGB) RGBTimer.Stop();
                        ParagraphBox.Focus();
                        break;
                    }
            }
        }
        private void DisableAllOptionsPanels()
        {
            Panel[] panles = { PanelSettingsDetails, PanelColorFillDetails, PanelAudioDetails, PanelKeyboardDetails, pnlLessonsList };
            foreach (Panel panel in panles)
            {
                Enable_Disable_Panel(panel, false);
            }
        }
        private void InitializeRGBcolors()
        {
            APPStatus.RGBVariables.random = new Random();
            APPStatus.RGBVariables.currentRed = 0;// Store current red value
            APPStatus.RGBVariables.currentGreen = 0;// Store current green value
            APPStatus.RGBVariables.currentBlue = 0;// Store current blue value
            APPStatus.RGBVariables.steps = 11 - RGBSpeedTrackBar.Value;// Number of steps for smooth transition
        }
        private void InitializeParagraphBoxLessons()
        {
            lblSparklingStars.Tag = "The night sky is a vast beautiful canvas dotted with sparkling stars These celestial bodies have" +
                " fascinated humans for centuries inspiring myths legends and scientific exploration Each star is a sun some much larger" +
                " and more luminous than our own Gazing at the stars can remind us of our place in the universe and the endless possibilities" +
                " that lie beyond our own world";
            lblTastyTreats.Tag = "Everyone loves a tasty treat now and then From rich creamy chocolates to fresh juicy fruits treats can bring" +
                " joy and satisfaction Baking cookies with loved ones savoring a slice of cake on a special occasion or simply enjoying a scoop of" +
                " ice cream on a hot day are all moments to cherish Treats not only satisfy our taste buds but also create memories and bring people" +
                " together";
            lblFirendshipPower.Tag = "Friendship is one of the most powerful and fulfilling relationships we can experience True friends support each" +
                " other through thick and thin offering a listening ear a helping hand and a comforting presence The bond of friendship is built on" +
                " trust respect and shared experiences Whether its laughing together over inside jokes or being there in times of need the power of" +
                " friendship enriches our lives";
            lblPracticeMakesPerfect.Tag = "The saying practice makes perfect holds a lot of truth Whether you are learning to play an instrument" +
                " mastering a new language or improving your typing skills consistent practice is key to success It is through repetition and dedication" +
                " that we refine our abilities and overcome challenges Remember that every expert was once a beginner who never gave up";
            lblCleverCrows.Tag = "Crows are incredibly smart birds known for their problem solving skills and use of tools " +
                "They can recognize human faces remember routes and even engage in complex social behaviors Studies have shown " +
                "that crows can solve puzzles that require insight and planning showcasing a level of intelligence that rivals some " +
                "primates Next time you see a crow remember that you are looking at one of natures cleverest creatures";
        }
        private void InitializeTypingResults()
        {
            APPStatus.TypingResults.totalKeystrokes = 0;
            APPStatus.TypingResults.correctKeystrokes = 0;
            APPStatus.TypingResults.incorrectKeystrokes = 0;
            APPStatus.TypingResults.startTime = DateTime.Now;
            APPStatus.TypingResults.lastKeystrokeTime = DateTime.Now;
        }
        private void UpdateMetrics()
        {
            TimeSpan elapsedTime = DateTime.Now - APPStatus.TypingResults.startTime;
            double minutes = elapsedTime.TotalMinutes;
            int wordCount = APPStatus.TypingResults.correctKeystrokes / 5; // Average word length of 5 characters
            // Calculate WPM and accuracy
            double wpm = minutes > 0 ? wordCount / minutes : 0;
            double accuracy = APPStatus.TypingResults.totalKeystrokes > 0 ?
                              (double)APPStatus.TypingResults.correctKeystrokes / APPStatus.TypingResults.totalKeystrokes * 100 : 0;
            // Ensure no fractions
            lblWPMnumber.Text = Math.Floor(wpm).ToString();
            lblAccuracyPercent.Text = Math.Floor(accuracy).ToString() + "%";
        }
        private void InitializeIllusionVariables()
        {
            IllusionTimer.Stop();
            APPStatus.IllusionVariables.RatingStartargetValue = 0;
            APPStatus.IllusionVariables.SpeedCircletargetValue = 0;
            APPStatus.IllusionVariables.DurationCircletargetValue = 0;
            APPStatus.IllusionVariables.AccuracyCircletargetValue = 0;
            RatingStar.Value = 0;
            SpeedCircle.Value = 0;
            SpeedCircle.Text = "";
            DurationCircle.Value = 0;
            DurationCircle.Text = "";
            AccuracyCircle.Value = 0;
        }
        private void InitializeSounds()
        {
            APPStatus.Sound.RighCharOrBackSpace = new SoundPlayer(Resources.RightOrBackSpace) ;
            APPStatus.Sound.WrongChar = new SoundPlayer(Resources.Wrong);
            APPStatus.Sound.Space = new SoundPlayer(Resources.Space);
        }
        private void ShowHidePanelSpeedAccuracy(bool Show)
        {
            Enable_Disable_Panel(PnlSpeedAccuracy, Show);
            lblWPM.Visible = Show;
            lblWPMnumber.Visible = Show;
            lblSpeed.Visible = Show;
            lblAccuracy.Visible = Show;
            lblAccuracyPercent.Visible = Show;
        }
        private void ResetLayout()
        {
            InitializeSounds();
            InitializeTypingResults();
            InitializeParagraphBoxLessons();
            InitializeIllusionVariables();
            ParagraphBox.Text = lblSparklingStars.Tag.ToString();
            lblLesson.Text = "الدرس : " + lblSparklingStars.Text.Substring(3, lblSparklingStars.Text.Length - 3);
            APPStatus.CurrentFormBackground = enCurrentFormBackground.Default;
            InitializeRGBcolors();
            APPStatus.charPosition = 0;
            ChangeAllFormTheme();
            ParagraphBox.Focus();
            APPStatus.ChoosedOption = enOptions.None;
            ToggleShowFastRate.Checked = true;
            ToggleBlockOnFault.Checked = false;
            ToggleListenKeysAudio.Checked = true;
            btnStandardLookKeyboard.PerformClick();
            this.BackColor = APPStatus.CurrentThemeColors.PBBack;
            APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.DefaultAfterLeaveColor;
            APPStatus.CurrentKeyboardTheme = enKeyboardThemes.Standard;
            toggleShowKeyboard.Checked = true;
            ToggleShowFastRate.Checked = true;
            DisableAllOptionsPanels();
            UpdatePanelKeyboardDetails();
            ShowHidePanelSpeedAccuracy(false);
            APPStatus.SeqWrongs = 0;
            ChangeCurrentActiveKey(ParagraphBox.Text[APPStatus.charPosition], false);
        }
        private void MainForm_Click(object sender, EventArgs e)
        {
            Button[] buttons = { btnSettings, btnFillColor, btnAudio, btnKeyboard, btnRefresh, btnPausePlay, btnMenu };
            foreach (Button btn in buttons)
            {
                ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
            }
            DisableAllOptionsPanels();
            APPStatus.ChoosedOption = enOptions.None;
            APPStatus.CurrentOpenedOptionButton = enOptions.None;
            APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.DefaultAfterLeaveColor;
            ParagraphBox.Focus();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSinceLastKeystroke = DateTime.Now - APPStatus.TypingResults.lastKeystrokeTime;
            if (timeSinceLastKeystroke.TotalSeconds >= 10)
            {
                if (btnPausePlay.Tag.ToString() == "Pause")
                {
                    btnPausePlay.PerformClick();
                }
            }
            UpdateMetrics();
        }
        private void ParagraphBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Prevent arrow key navigation
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Home || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.End)
            {
                e.Handled = true;
            }
        }
        private void ParagraphBox_MouseDown(object sender, MouseEventArgs e)
        {
            ParagraphBox.Select(APPStatus.charPosition, 1);
        }
        private void ParagraphBox_TextChanged(char Key, bool PrevColors)
        {
            if (PrevColors)
            {
                if (APPStatus.charPosition > 0)
                {
                    ChangeCurrentActiveKey(Key, PrevColors);
                }
            }
            if (APPStatus.charPosition + 1 < ParagraphBox.Text.Length)
                ChangeCurrentActiveKey(Key, PrevColors);
        }
        private void ChangeCurrentActiveKey(char Key, bool PrevColors)
        {
            ChangeKeyColor(FindLabelByChar(Key), PrevColors);
        }
        private void ResetKeyCapsExcepts(Label lbl)
        {
            foreach (Label LBL in pnlAllKeyBoard.Controls)
            {
                if (LBL == lbl) continue;
                else
                {
                    if (APPStatus.CurrentKeyboardTheme != enKeyboardThemes.Colorful)
                    {
                        LBL.ForeColor = APPStatus.CurrentThemeColors.KLFore;
                        LBL.BackColor = APPStatus.CurrentThemeColors.KLBack;
                    }
                    else
                    {
                        LBL.ForeColor = Color.White;
                        if (LBL == lblK1 || LBL == lblK2 || LBL == lblK15 || LBL == lblK16 || LBL == lblK29 || LBL == lblK30 || LBL == lblK42 || LBL == lblK43 ||
                            LBL == lblK11 || LBL == lblK12 || LBL == lblK13 || LBL == lblK14 || LBL == lblK25 || LBL == lblK26 || LBL == lblK27 || LBL == lblK28 ||
                            LBL == lblK39 || LBL == lblK40 || LBL == lblK41 || LBL == lblK52 || LBL == lblK53)
                        {
                            LBL.BackColor = Color.FromArgb(21, 122, 251); // BLUE
                        }
                        else if (LBL == lblK3 || LBL == lblK17 || LBL == lblK31 || LBL == lblK44 || LBL == lblK10 || LBL == lblK24 || LBL == lblK38 || LBL == lblK51)
                        {
                            LBL.BackColor = Color.FromArgb(127, 182, 49); // GREEN
                        }
                        else if (LBL == lblK4 || LBL == lblK18 || LBL == lblK32 || LBL == lblK45 || LBL == lblK9 || LBL == lblK23 || LBL == lblK37 || LBL == lblK50)
                        {
                            LBL.BackColor = Color.FromArgb(239, 167, 1); // YELLOW
                        }
                        else if (LBL == lblK54 || LBL == lblK55 || LBL == lblK56 || LBL == lblK57 || LBL == lblK58)
                        {
                            LBL.BackColor = Color.FromArgb(211, 211, 211); // Gray
                        }
                        else
                        {
                            LBL.BackColor = Color.FromArgb(251, 76, 76); // RED
                        }
                    }
                }
            }
        }
        private void ChangeKeyColor(Label lbl, bool UseLastColors)
        {
            ResetKeyCapsExcepts(lbl);
            if (UseLastColors)
            {
                lbl.ForeColor = APPStatus.LastForeColor;
                lbl.BackColor = APPStatus.LastBackColor;
            }
            else
            {
                APPStatus.LastForeColor = lbl.ForeColor;
                APPStatus.LastBackColor = lbl.BackColor;
                lbl.ForeColor = Color.White;
                if (APPStatus.CurrentKeyboardTheme == enKeyboardThemes.Colorful)
                {
                    lbl.BackColor = Color.Gray;
                }
                else
                {
                    lbl.BackColor = Color.FromArgb(121, 187, 255);
                }
            }
        }
        private Label FindLabelByChar(char keyChar)
        {
            if (keyChar == (char)Keys.Space) return lblK56;
            if (keyChar == (char)8) return lblK14;
            if (keyChar == (char)Keys.RShiftKey) return lblK42;
            if (keyChar == (char)Keys.LShiftKey) return lblK53;
            foreach (Label lbl in pnlAllKeyBoard.Controls)
            {
                if (lbl.Text.ToString().ToUpper() == keyChar.ToString().ToUpper())
                {
                    return lbl;
                }
            }
            return null;
        }
        private async void ChangeRightWrongColor(Label label, Color clr)
        {
            Color PrevFore = new Color();
            if (APPStatus.CurrentKeyboardTheme != enKeyboardThemes.RGB)
                PrevFore = label.ForeColor;
            Color PrevBack = label.BackColor;
            if (clr == Color.FromArgb(131, 251, 211)) label.ForeColor = Color.White;
            label.BackColor = clr;
            await Task.Delay(200);
            if (APPStatus.CurrentKeyboardTheme != enKeyboardThemes.RGB)
                label.ForeColor = PrevFore;
            label.BackColor = PrevBack;
        }
        private void KeyInputColorsValidating(char Key, bool Right)
        {
            Color clr = new Color();
            if (Right && APPStatus.CurrentKeyboardTheme == enKeyboardThemes.Colorful) return;
            if (Right) clr = Color.FromArgb(231, 251, 211);
            else clr = Color.FromArgb(233, 126, 126);
            if (Key == (char)Keys.Space) ChangeRightWrongColor(lblK56, clr);
            else if (Key == (char)8) ChangeRightWrongColor(lblK14, clr);
            else if (Key == (char)Keys.RShiftKey) ChangeRightWrongColor(lblK42, clr);
            else if (Key == (char)Keys.LShiftKey) ChangeRightWrongColor(lblK53, clr);
            else ChangeRightWrongColor(FindLabelByChar(Key), clr);
        }
        private bool IsArabic(char c)
        {
            return (c >= 0x0600 && c <= 0x06FF) || // Arabic
                   (c >= 0x0750 && c <= 0x077F) || // Arabic Supplement
                   (c >= 0x08A0 && c <= 0x08FF) || // Arabic Extended-A
                   (c >= 0xFB50 && c <= 0xFDFF) || // Arabic Presentation Forms-A
                   (c >= 0xFE70 && c <= 0xFEFF);   // Arabic Presentation Forms-B
        }
        private bool AcceptedKeys(KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || (e.KeyChar == ' ') || e.KeyChar == (char)8) return true;
            return false;
        }
        private void ParagraphBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (btnPausePlay.Tag.ToString() == "Play")
            {
                btnPausePlay.PerformClick();
            }
            if (APPStatus.charPosition == 0 && APPStatus.TypingResults.totalKeystrokes == 0)
            {
                APPStatus.TypingResults.startTime = DateTime.Now;
                Timer.Start();
            }
            APPStatus.TypingResults.totalKeystrokes++;
            APPStatus.TypingResults.lastKeystrokeTime = DateTime.Now; // Update last keystroke time
            if (IsArabic(e.KeyChar))
            {
                // Prevent Arabic input
                e.Handled = true;
                MessageBox.Show("Please Change Your Keyboard To English", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (APPStatus.charPosition >= 10)
            {
                btnPausePlay.Show();
                if (ToggleShowFastRate.Checked)
                {
                    ShowHidePanelSpeedAccuracy(true);
                }
            }
            e.Handled = true; // stop the default beep sound cause the parabox set to read only
            char PressedChar = e.KeyChar;
            ParagraphBox.Select(APPStatus.charPosition, 1);
            if (AcceptedKeys(e))
            {
                if (PressedChar != (char)Keys.Back && PressedChar != ParagraphBox.Text[APPStatus.charPosition] && ToggleBlockOnFault.Checked)
                {
                    if (ToggleListenKeysAudio.Checked) APPStatus.Sound.WrongChar.Play();
                    KeyInputColorsValidating(PressedChar, false);
                    APPStatus.SeqWrongs += 1;
                    ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBWrongCharFore;
                    ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBWrongCharBack;
                    if (APPStatus.SeqWrongs == 10)
                    {
                        ManageRefreshScreen();
                        return;
                    }
                    return;
                }
                if (PressedChar == (char)Keys.Back)
                {
                    if (APPStatus.charPosition > 0)
                    {
                        APPStatus.charPosition -= 1;
                        if (ToggleListenKeysAudio.Checked)
                            APPStatus.Sound.RighCharOrBackSpace.Play();
                    }
                    else
                    {
                        SystemSounds.Beep.Play();
                        return;
                    }
                    ParagraphBox.Select(APPStatus.charPosition, 1);// 1 => char length
                    if (APPStatus.charPosition >= 0)
                    {
                        ParagraphBox_TextChanged(ParagraphBox.Text[APPStatus.charPosition + 1], true);
                        ParagraphBox_TextChanged(ParagraphBox.Text[APPStatus.charPosition], false);
                    }
                }
                else
                {
                    ParagraphBox.Select(APPStatus.charPosition + 1, 1);
                    ParagraphBox_TextChanged(ParagraphBox.Text[APPStatus.charPosition], true);
                    if (APPStatus.charPosition < ParagraphBox.Text.Length - 1)
                    {
                        ParagraphBox_TextChanged(ParagraphBox.Text[APPStatus.charPosition + 1], false);
                    }
                    if (PressedChar == ParagraphBox.Text[APPStatus.charPosition])
                    {
                        APPStatus.TypingResults.correctKeystrokes++;
                        KeyInputColorsValidating(PressedChar, true);
                        if (ToggleListenKeysAudio.Checked)
                        {
                            if (PressedChar == ' ') APPStatus.Sound.Space.Play();
                            else APPStatus.Sound.RighCharOrBackSpace.Play();
                        }
                        ParagraphBox.Select(APPStatus.charPosition, 1);
                        if (ParagraphBox.SelectionBackColor == APPStatus.CurrentThemeColors.PBWrongCharBack ||
                            ParagraphBox.SelectionBackColor == APPStatus.CurrentThemeColors.PBCorrectionCharBack)
                        {
                            ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBCorrectionCharFore;
                            ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBCorrectionCharBack;
                        }
                        else
                        {
                            ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBRightCharFore;
                            ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBRightCharBack;
                            APPStatus.SeqWrongs = 0;
                        }
                    }
                    else
                    {
                        KeyInputColorsValidating(PressedChar, false);
                        APPStatus.SeqWrongs += 1;
                        if (ToggleListenKeysAudio.Checked)
                            APPStatus.Sound.WrongChar.Play();
                        ParagraphBox.Select(APPStatus.charPosition, 1);
                        ParagraphBox.SelectionColor = APPStatus.CurrentThemeColors.PBWrongCharFore;
                        ParagraphBox.SelectionBackColor = APPStatus.CurrentThemeColors.PBWrongCharBack;
                        if (APPStatus.SeqWrongs == 10)
                        {
                            ManageRefreshScreen();
                            return;
                        }
                    }
                    ParagraphBox.Select(APPStatus.charPosition + 1, 1);
                    if (APPStatus.charPosition + 1 < ParagraphBox.Text.Length) APPStatus.charPosition++;
                    else
                    {
                        RGBTimer.Stop();
                        Timer.Stop();
                        ManageFinalScreen();
                    }
                }
            }
        }
        private void IllusionTimer_Tick(object sender, EventArgs e)
        {
            if (RatingStar.Value < APPStatus.IllusionVariables.RatingStartargetValue)
            {
                RatingStar.Value += 0.4f;
            }
            else
            {
                RatingStar.Value = APPStatus.IllusionVariables.RatingStartargetValue;
            }
            //
            if (SpeedCircle.Value < APPStatus.IllusionVariables.SpeedCircletargetValue)
            {
                SpeedCircle.Value += 3;
                SpeedCircle.Text = SpeedCircle.Value + " WPM";

            }
            else
            {
                SpeedCircle.Value = APPStatus.IllusionVariables.SpeedCircletargetValue;
                SpeedCircle.Text = lblWPMnumber.Text + " WPM";
            }
            //
            if (DurationCircle.Value < APPStatus.IllusionVariables.DurationCircletargetValue)
            {
                DurationCircle.Value += 2;
            }
            else
            {
                DurationCircle.Value = APPStatus.IllusionVariables.DurationCircletargetValue;
            }
            //
            if (AccuracyCircle.Value < APPStatus.IllusionVariables.AccuracyCircletargetValue)
            {
                AccuracyCircle.Value += 5;
            }
            else
            {
                RatingStar.Value = APPStatus.IllusionVariables.AccuracyCircletargetValue;
            }
            //
            if (RatingStar.Value == APPStatus.IllusionVariables.RatingStartargetValue && SpeedCircle.Value == APPStatus.IllusionVariables.SpeedCircletargetValue
                && DurationCircle.Value == APPStatus.IllusionVariables.DurationCircletargetValue && AccuracyCircle.Value == APPStatus.IllusionVariables.AccuracyCircletargetValue)
            {
                IllusionTimer.Stop();
            }
        }
        private void SetStars()
        {
            float sum = 0;
            sum += Convert.ToInt16(lblWPMnumber.Text);
            sum += Convert.ToInt16(lblAccuracyPercent.Text.Substring(0, lblAccuracyPercent.Text.Length - 1));
            double duration = 0;
            TimeSpan elapsedTime = APPStatus.TypingResults.lastKeystrokeTime - APPStatus.TypingResults.startTime;
            duration += elapsedTime.Seconds / 60F;
            duration += Convert.ToDouble(elapsedTime.Minutes);
            sum += (float)(100F - (duration / 10F * 100F));
            sum /= 3F;
            APPStatus.IllusionVariables.RatingStartargetValue = (((sum / 20F) - (int)(sum / 20F)) * 10) >= 5F ? ((int)(sum / 20F) + 0.5F) : (sum / 20F);
        }
        private void SetSpeedCircle()
        {
            short value = Convert.ToByte(lblWPMnumber.Text);
            Color clr = new Color();
            Color clr2 = new Color();
            if (value >= 1 && value <= 10)
            {
                clr = Color.Red;
                clr2 = Color.OrangeRed;
            }
            else if (value > 10 && value <= 15)
            {
                clr = Color.OrangeRed;
                clr2 = Color.Orange;
            }
            else if (value > 15 && value <= 20)
            {
                clr = Color.Orange;
                clr2 = Color.Yellow;
            }
            else if (value > 20 && value <= 25)
            {
                clr = Color.Yellow;
                clr2 = Color.YellowGreen;
            }
            else
            {
                clr = Color.YellowGreen;
                clr2 = Color.Green;
            }
            SpeedCircle.ProgressColor = clr;
            SpeedCircle.ProgressColor2 = clr2;
            lblFinalspeed.ForeColor = clr2;
            APPStatus.IllusionVariables.SpeedCircletargetValue = value;
        }
        private void SetDurationCircle()
        {
            TimeSpan elapsedTime = APPStatus.TypingResults.lastKeystrokeTime - APPStatus.TypingResults.startTime;
            DurationCircle.Text = elapsedTime.Minutes.ToString() + ":" + elapsedTime.Seconds.ToString() + "\nث - د";
            double duration = 0;
            duration += elapsedTime.Seconds / 60D;
            duration += Convert.ToDouble(elapsedTime.Minutes);
            duration = duration / 10F * 100F;
            float value = (elapsedTime.Minutes * 60 + elapsedTime.Seconds);
            Color clr = new Color();
            Color clr2 = new Color();
            if (value >= 1 && value <= 90)
            {
                clr = Color.YellowGreen;
                clr2 = Color.Green;
            }
            else if (value > 90 && value < 120)
            {
                clr = Color.Yellow;
                clr2 = Color.YellowGreen;
            }
            else if (value >= 120 && value < 180)
            {
                clr = Color.Orange;
                clr2 = Color.Yellow;
            }
            else if (value >= 180 && value <= 300)
            {
                clr = Color.OrangeRed;
                clr2 = Color.Orange;
            }
            else if (value > 300)
            {
                clr = Color.Red;
                clr2 = Color.OrangeRed;
            }
            DurationCircle.ProgressColor = clr;
            DurationCircle.ProgressColor2 = clr2;
            lblDuration.ForeColor = clr2;
            APPStatus.IllusionVariables.DurationCircletargetValue = duration - (int)duration >= .5 ? (short)duration++ : (short)duration;
        }
        private void SetAccuracyCircle()
        {
            short value = Convert.ToInt16(lblAccuracyPercent.Text.Substring(0, lblAccuracyPercent.Text.Length - 1));
            Color clr = new Color();
            Color clr2 = new Color();
            if (value >= 50 && value < 70)
            {
                clr = Color.OrangeRed;
                clr2 = Color.Orange;
            }
            else if (value >= 70 && value <= 80)
            {
                clr = Color.Orange;
                clr2 = Color.Yellow;
            }
            else if (value > 80 && value <= 90)
            {
                clr = Color.Yellow;
                clr2 = Color.YellowGreen;
            }
            else if (value > 90 && value <= 100)
            {
                clr = Color.YellowGreen;
                clr2 = Color.Green;
            }
            else
            {
                clr = Color.Red;
                clr2 = Color.OrangeRed;
            }
            AccuracyCircle.ProgressColor = clr;
            AccuracyCircle.ProgressColor2 = clr2;
            lblFinalScrPersice.ForeColor = clr2;
            APPStatus.IllusionVariables.AccuracyCircletargetValue = value;
        }
        private void ManageFinalScreen()
        {
            Enable_Disable_Panel(pnlFInal, true);
            DisableEnableEverythingExcept(false, pnlFInal);
            SetStars();
            SetSpeedCircle();
            SetDurationCircle();
            SetAccuracyCircle();
            IllusionTimer.Start();
        }
        private void ManageRefreshScreen()
        {
            Enable_Disable_Panel(pnlFocus, true);
            DisableEnableEverythingExcept(false, pnlFocus);
        }
        private void DisableEnableEverythingExcept(bool Enable, Control control)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl == control || ctrl == control || ctrl == control) continue;
                ctrl.Enabled = Enable;
            }
        }
        private void REFRESH()
        {
            Timer.Stop();
            IllusionTimer.Stop();
            Enable_Disable_Panel(pnlFInal, false);
            Enable_Disable_Panel(pnlFocus, false);
            InitializeIllusionVariables();
            lblWPMnumber.Text = "";
            lblAccuracyPercent.Text = "";
            PnlSpeedAccuracy.Hide();
            APPStatus.SeqWrongs = 0;
            InitializeTypingResults();
            APPStatus.LastChoosed = null;
            DisableAllOptionsPanels();
            ChangeAllControlsColors();
            btnPausePlay.Tag = "Pause";
            btnPausePlay.Hide();
            ChangeOptionsButtonImage(btnPausePlay, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
            APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.DefaultAfterLeaveColor;
            APPStatus.CurrentOpenedOptionButton = enOptions.None;
            APPStatus.ChoosedOption = enOptions.None;
            APPStatus.charPosition = 0;
            ResetParagraphBoxColors();
            ShowHidePanelSpeedAccuracy(false);
            ParagraphBox.Select(APPStatus.charPosition, 1);
            ChangeCurrentActiveKey(ParagraphBox.Text[APPStatus.charPosition], false);
            ParagraphBox.Focus();
        }
        private void Show_Hide_AllKeyboard(bool Show = true)
        {
            if (!Show)
            {
                ParagraphBox.Location = new Point(270, 245); // Sets X to 270 and Y to 100
                ParagraphBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                PanelKeyboardDetails.Height = 83;
            }
            else
            {
                ParagraphBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                ParagraphBox.Location = new Point(270, 95);
                if (APPStatus.CurrentKeyboardTheme == enKeyboardThemes.RGB)
                    PanelKeyboardDetails.Height = 337;
                else
                    PanelKeyboardDetails.Height = 266;
            }
            pnlAllKeyBoard.Visible = Show;
        }
        private void UpdatePanelKeyboardDetails()
        {
            lblKeyboardTheme.Visible = toggleShowKeyboard.Checked;
            btnRGBLookKeyboard.Visible = toggleShowKeyboard.Checked;
            btnStandardLookKeyboard.Visible = toggleShowKeyboard.Checked;
            btnColorfulLookKeyboard.Visible = toggleShowKeyboard.Checked;
            RGBSpeedTrackBar.Visible = toggleShowKeyboard.Checked;
            lblRGBSpeed.Visible = toggleShowKeyboard.Checked;
            pnlAllKeyBoard.Visible = toggleShowKeyboard.Checked;
            Show_Hide_AllKeyboard(toggleShowKeyboard.Checked);
        }
        private void toggleShowKeyboard_Click(object sender, EventArgs e)
        {
            UpdatePanelKeyboardDetails();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ResetLayout();
        }
        private void Change_Key_Cap_Theme_To_ColorFul()
        {
            lblK55.Image = Resources.WhiteWindows_11;
            foreach (Label LBL in pnlAllKeyBoard.Controls)
            {
                LBL.ForeColor = Color.White;
                if (LBL == lblK1 || LBL == lblK2 || LBL == lblK15 || LBL == lblK16 || LBL == lblK29 || LBL == lblK30 || LBL == lblK42 || LBL == lblK43 ||
                    LBL == lblK11 || LBL == lblK12 || LBL == lblK13 || LBL == lblK14 || LBL == lblK25 || LBL == lblK26 || LBL == lblK27 || LBL == lblK28 ||
                    LBL == lblK39 || LBL == lblK40 || LBL == lblK41 || LBL == lblK52 || LBL == lblK53)
                {
                    LBL.BackColor = Color.FromArgb(21, 122, 251); // BLUE
                }
                else if (LBL == lblK3 || LBL == lblK17 || LBL == lblK31 || LBL == lblK44 || LBL == lblK10 || LBL == lblK24 || LBL == lblK38 || LBL == lblK51)
                {
                    LBL.BackColor = Color.FromArgb(127, 182, 49); // GREEN
                }
                else if (LBL == lblK4 || LBL == lblK18 || LBL == lblK32 || LBL == lblK45 || LBL == lblK9 || LBL == lblK23 || LBL == lblK37 || LBL == lblK50)
                {
                    LBL.BackColor = Color.FromArgb(239, 167, 1); // YELLOW
                }
                else if (LBL == lblK54 || LBL == lblK55 || LBL == lblK56 || LBL == lblK57 || LBL == lblK58)
                {
                    LBL.BackColor = Color.FromArgb(211, 211, 211); // Gray
                }
                else
                {
                    LBL.BackColor = Color.FromArgb(251, 76, 76); // RED
                }
            }
        }
        private void UpdateKeyboardTheme(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string btnTag = btn.Tag.ToString();
            if ((btnTag == "RGB" && APPStatus.CurrentKeyboardTheme == enKeyboardThemes.RGB) ||
               (btnTag == "Colorful" && APPStatus.CurrentKeyboardTheme == enKeyboardThemes.Colorful) ||
               (btnTag == "Standard" && APPStatus.CurrentKeyboardTheme == enKeyboardThemes.Standard)) return;
            else
            {
                if (btnTag == "RGB")
                {
                    APPStatus.CurrentKeyboardTheme = enKeyboardThemes.RGB;
                    Disable_Enable_RGB_Speed_TrackBar(true);
                    if (APPStatus.CurrentKeyboardTheme != enKeyboardThemes.Standard)
                        ResetKeyCapsColors();
                    RGBTimer.Start();
                }
                else if (btnTag == "Colorful")
                {
                    Disable_Enable_RGB_Speed_TrackBar(false);
                    RGBTimer.Stop();
                    APPStatus.CurrentKeyboardTheme = enKeyboardThemes.Colorful;
                    Change_Key_Cap_Theme_To_ColorFul();
                }
                else // standard theme
                {
                    Disable_Enable_RGB_Speed_TrackBar(false);
                    APPStatus.CurrentKeyboardTheme = enKeyboardThemes.Standard;
                    RGBTimer.Stop();
                    ResetKeyCapsColors();
                }
                ChangeCurrentActiveKey(ParagraphBox.Text[APPStatus.charPosition], false);
            }
        }
        private void Disable_Enable_RGB_Speed_TrackBar(bool Enable = true)
        {
            if (APPStatus.CurrentKeyboardTheme == enKeyboardThemes.RGB)
            {
                if (Enable)
                {
                    RGBSpeedTrackBar.Visible = true;
                    lblRGBSpeed.Visible = true;
                    PanelKeyboardDetails.Height = 337;
                }
                else
                {
                    RGBSpeedTrackBar.Visible = false;
                    lblRGBSpeed.Visible = false;
                    PanelKeyboardDetails.Height = 266;
                }
            }
        }
        private void LessonsPanelLabels_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(66, 153, 250);
        }
        private void LessonsPanelLabels_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = APPStatus.CurrentThemeColors.AllPanelsFore;
        }
        private void Any_btnOption_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnTag = btn.Tag.ToString();
            if (APPStatus.CurrentOptionButtonColor == enCurrentOptionButtonColor.MouseEnterColor)
            {
                if (APPStatus.CurrentOpenedOptionButton.ToString() != btnTag)
                {
                    ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.OptDefaultORMouseLeaveBackIMG);
                }
            }
        }
        private void AnyOptionBtn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnTag = btn.Tag.ToString();
            if (APPStatus.CurrentOptionButtonColor == enCurrentOptionButtonColor.DefaultAfterLeaveColor ||
                APPStatus.CurrentOpenedOptionButton.ToString() != btnTag)
            {
                ChangeOptionsButtonImage(btn, APPStatus.CurrentThemeColors.OptMouseEnterBackIMG);
                APPStatus.CurrentOptionButtonColor = enCurrentOptionButtonColor.MouseEnterColor;
            }
        }
        private void ResetSizeButtonsExcept(Guna2Button ExceptedBtn)
        {
            if (btnSmallFontSize != ExceptedBtn)
            {
                btnSmallFontSize.Checked = false;
            }
            if (btnMediumFontSize != ExceptedBtn)
            {
                btnMediumFontSize.Checked = false;
            }
            if (btnLargeFontSize != ExceptedBtn)
            {
                btnLargeFontSize.Checked = false;
            }
        }
        private void UpdateParagraphBoxFontSize(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            if (ParagraphBox.Font.Size.ToString() == btn.Tag.ToString()) return;
            int newSize;
            if (btn == btnSmallFontSize)
            {
                ResetSizeButtonsExcept(btnSmallFontSize);
                newSize = 20;
            }
            else if (btn == btnMediumFontSize)
            {
                ResetSizeButtonsExcept(btnMediumFontSize);
                newSize = 22;
            }
            else
            {
                ResetSizeButtonsExcept(btnLargeFontSize);
                newSize = 24;
            }
            ParagraphBox.Font = new Font(ParagraphBox.Font.FontFamily, newSize, ParagraphBox.Font.Style);
            ResetParagraphBoxColors();
        }
        private void Any_Control_Escape_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode != Keys.Tab) MainForm_Click(sender, e as EventArgs);
        }
        private void lblLessons_Click(object sender, EventArgs e)
        {
            string text = (sender as Label).Tag as string;
            ParagraphBox.Text = text;
            lblLesson.Text = "الدرس: " + (sender as Label).Text.Substring(3, (sender as Label).Text.Length - 3);
            APPStatus.SeqWrongs = 0;
            APPStatus.charPosition = 0;
            ParagraphBox.Select(APPStatus.charPosition, 1);
            ResetKeyCapsColors();
            ChangeCurrentActiveKey(ParagraphBox.Text[APPStatus.charPosition], false);
        }
        private void RGBSpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            APPStatus.RGBVariables.steps = 11 - RGBSpeedTrackBar.Value;
        }
        private void btnRestartLesson_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).ForeColor = Color.FromArgb(25, 25, 25);
            (sender as Button).BackColor = Color.White;
        }
        private void btnRestartLesson_MouseEnter(object sender, EventArgs e)
        {
            (sender as Button).ForeColor = Color.White;
            (sender as Button).BackColor = Color.FromArgb(25, 25, 25);
        }
        private void btnRestartLesson_Click(object sender, EventArgs e)
        {
            DisableEnableEverythingExcept(true, null);
            REFRESH();
        }
        private void ExitBTN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void RGBTimer_Tick(object sender, EventArgs e)
        {
            if (APPStatus.CurrentKeyboardTheme == enKeyboardThemes.RGB)  // rgb keycabs
            {
                int targetRed = APPStatus.RGBVariables.random.Next(256); // Generate random red value
                int targetGreen = APPStatus.RGBVariables.random.Next(256); // Generate random green value
                int targetBlue = APPStatus.RGBVariables.random.Next(256); // Generate random blue value
                int redDelta = targetRed - APPStatus.RGBVariables.currentRed; // Calculate difference in red
                int greenDelta = targetGreen - APPStatus.RGBVariables.currentGreen; // Calculate difference in green
                int blueDelta = targetBlue - APPStatus.RGBVariables.currentBlue; // Calculate difference in blue
                int redIncrement = redDelta / APPStatus.RGBVariables.steps; // Amount of red change per tick
                int greenIncrement = greenDelta / APPStatus.RGBVariables.steps; // Amount of green change per tick
                int blueIncrement = blueDelta / APPStatus.RGBVariables.steps; // Amount of blue change per tick
                APPStatus.RGBVariables.currentRed += redIncrement; // Update current red value
                APPStatus.RGBVariables.currentGreen += greenIncrement; // Update current green value
                APPStatus.RGBVariables.currentBlue += blueIncrement; // Update current blue value
                foreach (Label LBL in pnlAllKeyBoard.Controls)
                {
                    if (LBL == lblK55) continue;
                    if (LBL != FindLabelByChar(ParagraphBox.Text[APPStatus.charPosition]))
                        LBL.ForeColor = Color.FromArgb(255, APPStatus.RGBVariables.currentRed, APPStatus.RGBVariables.currentGreen, APPStatus.RGBVariables.currentBlue);
                }
                lblK55.Image = Resources.Windows_11;
            }
        }
        private void ToggleShowFastRate_CheckedChanged(object sender, EventArgs e)
        {
            ShowHidePanelSpeedAccuracy(ToggleShowFastRate.Checked);
        }
    } 
}