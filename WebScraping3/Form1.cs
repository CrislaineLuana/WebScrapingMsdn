using System.Net;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraping3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AtualizarButton_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            dataGridView1.Rows.Clear();
            for (int i = 1; i <= 3; i++) {         
                
                string pagina = wc.DownloadString($"https://social.msdn.microsoft.com/Forums/pt-BR/home?sort=lastpostdesc&brandIgnore=true&page={i}");
                string url = "https://social.msdn.microsoft.com/Forums/pt-BR/home";

            

                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(pagina);

                

                string id = string.Empty;
                string titulo = string.Empty;
                string postagem = string.Empty;
                string exibicao = string.Empty;
                string resposta = string.Empty;
                string link = string.Empty;

                foreach (HtmlNode node in htmlDocument.GetElementbyId("threadList").ChildNodes )
                {
                    if(node.Attributes.Count > 0)
                    {
                        id = node.Attributes["data-threadid"].Value;
                        link = "https://social.msdn.microsoft.com/Forums/pt-BR/" + id;
                        titulo = node.Descendants().First(x => x.Id.Equals("threadTitle_" + id)).InnerText;
                        postagem = node.Descendants().First(x => x.Attributes["class"] != null &&  x.Attributes["class"].Value.Equals("lastpost")).InnerText.Replace("\n", "").Replace("  ", "");
                        exibicao = node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("viewcount")).InnerText;
                        resposta = node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("replycount")).InnerText;

                        if (!string.IsNullOrEmpty(titulo))
                        {
                            dataGridView1.Rows.Add(titulo, postagem, exibicao, resposta, link);
                        }
                    } 
              
                };
            }
        }
    }
}