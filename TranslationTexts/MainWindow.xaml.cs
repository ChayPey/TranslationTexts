using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TranslatorAPI;

namespace TranslationTexts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Languages languages;
        private Translator translator;
        public MainWindow()
        {
            InitializeComponent();

            string APIKey = "trnsl.1.1.20200421T094203Z.8f9595c509cb9fc9.0b95698bf5cc031b04cc33a1099595b862c1af1a";
            translator = new Translator(APIKey);
            languages = translator.SupportedLanguages();
            languages.langs = languages.langs.ToDictionary(x => x.Value, x => x.Key);

            foreach (string p in languages.langs.Keys)
            {
                ComboBoxText.Items.Add(p);
                ComboBoxTranslation.Items.Add(p);
            }
            ComboBoxText.Text = "Русский";
            ComboBoxTranslation.Text = "Английский";
            //TextBoxTranslation.Text = translator.Translate(TextBoxText.Text, languages.langs[ComboBoxText.Text], languages.langs[ComboBoxTranslation.Text]).text[0];
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TextBoxTranslation.Text = translator.Translate(TextBoxText.Text, languages.langs[ComboBoxText.Text], languages.langs[ComboBoxTranslation.Text]).text[0];
        }

        private void swap_text_Click(object sender, RoutedEventArgs e)
        {
            string buffer = TextBoxTranslation.Text;
            TextBoxTranslation.Text = TextBoxText.Text;
            TextBoxText.Text = buffer;
            buffer = ComboBoxText.Text;
            ComboBoxText.Text = ComboBoxTranslation.Text;
            ComboBoxTranslation.Text = buffer;

        }
    }
}
