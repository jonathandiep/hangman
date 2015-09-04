using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Hangman
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hide.Text = HiddenAnswer;
            hangman.ImageUrl = "Images/" + Guesses.ToString() + ".gif";
            round.Text = "Guesses Left: " + (6 - Guesses);
        }

        protected string Answer
        {
            get
            {
                if (Session["Answer"] == null)
                    Session["Answer"] = GetRandomAnswer();
                return (string)Session["Answer"];
            }
        }

        protected string HiddenAnswer
        {
            get
            {
                if (Session["HiddenAnswer"] == null)
                {
                    foreach (var letter in Answer)
                    {
                        Session["HiddenAnswer"] += "*";
                    }
                }
                return (string)Session["HiddenAnswer"];
            }
        }

        protected string UserEntry
        {
            get
            {
                Session["UserEntry"] = userInput.Text;
                return (string)Session["UserEntry"];
            }
        }

        protected string Difficulty
        {
            get
            {
                Session["Difficulty"] = rblist.SelectedValue;
                return (string)Session["Difficulty"];
            }
        }

        protected int Guesses
        {
            get
            {
                if (Session["Guesses"] == null)
                {
                    if (Difficulty == "hard")
                    {
                        Session["Guesses"] = 4;
                    }
                    else if (Difficulty == "medium")
                    {
                        Session["Guesses"] = 2;
                    }
                    else
                    {
                        Session["Guesses"] = 0;
                    }
                }
                return (int)Session["Guesses"];
            }
            set
            {
                Session["Guesses"] = value;
            }

        }

        private string GetRandomAnswer()
        {
            string dataAnswers;
            if (Difficulty == "easy")
            {
                dataAnswers = File.ReadAllText(Server.MapPath("~/Answers/easy.txt"));
            }
            else if (Difficulty == "medium")
            {
                dataAnswers = File.ReadAllText(Server.MapPath("~/Answers/medium.txt"));
            }
            else
            {
                dataAnswers = File.ReadAllText(Server.MapPath("~/Answers/hard.txt"));
            }

            List<string> answers = dataAnswers.Split(',').ToList();
            Random rand = new Random();
            string word = answers[rand.Next(0, answers.ToArray().Length)];
            return word;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            statusLabel.ForeColor = System.Drawing.Color.Black;
            statusLabel.Font.Bold = false;
            if (Guesses == 5)
            {
                statusLabel.Text = "YOU LOSE";
                Guesses++;
                statusLabel.ForeColor = System.Drawing.Color.Red;
                statusLabel.Font.Bold = true;
            }
            else if(userInput.Text.Length > 1)
            {
                if (userInput.Text.ToUpper() == Answer.ToUpper())
                {
                    Session["HiddenAnswer"] = Answer;
                    statusLabel.Text = "YOU WIN";
                    statusLabel.ForeColor = System.Drawing.Color.Green;
                    statusLabel.Font.Bold = true;
                }
                else
                {
                    statusLabel.Text = "ur bad at guessing lol";
                    Guesses++;
                }
            }
            else
            {
                if (userInput.Text == "")
                {
                    statusLabel.Text = "u didn't fill anything but you submitted so u lose a point lol";
                        Guesses++;
                }
                else if(Answer.Contains(userInput.Text))
                {
                    statusLabel.Text = "good guess";
                    List<string> savedAnswer = new List<string>();
                    foreach (char letter in HiddenAnswer)
                    {
                        savedAnswer.Add(letter.ToString());
                    }
                    List<string> ans = new List<string>();
                    foreach (char letter in Answer)
                    {
                        if (userInput.Text == letter.ToString())
                        {
                            ans.Add(letter.ToString());
                        }
                        else
                        {
                            ans.Add("*");
                        }
                    }
                    List<string> compiledAnswer = new List<string>();
                    string[] savedAnswerArray = savedAnswer.ToArray();
                    string[] ansArray = ans.ToArray();
                    for (var i = 0; i < ansArray.Length; i++)
                    {
                        if (savedAnswer[i] == ans[i])
                        {
                            compiledAnswer.Add("*");
                        }
                        else
                        {
                            if (savedAnswer[i] == "*")
                            {
                                compiledAnswer.Add(ans[i]);
                            }
                            else
                            {
                                compiledAnswer.Add(savedAnswer[i]);
                            }
                        }
                    }
                    string output = string.Join("", compiledAnswer.ToArray());
                    Session["HiddenAnswer"] = output;
                }
                else
                {
                    statusLabel.Text = "ur bad at guessing lol";
                    Guesses++;
                }
            }
            if (HiddenAnswer == Answer)
            {
                statusLabel.Text = "YOU WIN";
            }
            userInput.Text = string.Empty;
            Page_Load(null, EventArgs.Empty);
        }

        protected void change_Difficulty(object sender, EventArgs e)
        {
            Session["Answer"] = GetRandomAnswer();
            Session["HiddenAnswer"] = null;
            Session["Difficulty"] = null;
            Session["Guesses"] = null;
            Session["ComparedVariable"] = null;
            statusLabel.Text = string.Empty;
            userInput.Text = string.Empty;
            Page_Load(null, EventArgs.Empty);
        }
    }
}