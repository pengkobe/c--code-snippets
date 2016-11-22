using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using Emgu.CV.UI;

namespace MultiFaceRec
{
    public partial class FrmPrincipal : Form
    {
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, namess = null, names = null;
        Dictionary<string, Rectangle> foundPeople = new Dictionary<string, Rectangle>();
        float xfactor;
        float yfactor;

        /// <summary>
        /// 初始化
        /// </summary>
        public FrmPrincipal()
        {
            InitializeComponent();

            try
            {
                // 初始化摄像头
                grabber = new Capture();
                grabber.QueryFrame();
                //初始化帧接收器事件（FrameGraber）
                Application.Idle += new EventHandler(FrameGrabber);
                if (grabber != null)
                    grabber.FlipHorizontal = !grabber.FlipHorizontal;
                button1.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("没有摄像头！");
            }

            // 加载人脸识别特征描述文件
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            // 识别眼睛
            //eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                // 加载已经训练过的图片与标签
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;

                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.ToString());
                MessageBox.Show("暂没有人脸！", "加载人脸...",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Idle += new EventHandler(FrameGrabber);                
                button1.Enabled = false;
            }
            catch (Exception)
            {                
            }                
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Idle -= new EventHandler(FrameGrabber);
                button1.Enabled = true;
            }
            catch (Exception)
            {
            }   
        }

        // 获取头像
        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                // 脸计数器
                ContTrain = ContTrain + 1;

                // 从摄像头得到灰色图片，大小可以调节,如调成260,240，可以减少cpu计算量
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                // 人脸检测
                // 下列方法已过时，建议使用HaarCascade的Detect方法
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new Size(20, 20));

                // 处理检测到的人脸
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                    break;
                }

                // 调整人脸图片大小以便进行比较
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(textBox1.Text);

                // 显示添加的人脸
                imageBox1.Image = TrainedFace;

                // 将人脸信息写到文本文件中
                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                // 保存人脸信息
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                }

                MessageBox.Show(textBox1.Text + "´s face detected and added :)", "训练成功", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "训练失败", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 人脸识别与检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            //label4.Text = "";
            NamePersons.Add("");

            //得到摄像头当前帧
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //转换格式
            gray = currentFrame.Convert<Gray, Byte>();

            // 人脸检测
            // MCvAvgComp[] facesDetected1 =  face.Detect(gray, 1.2, 10, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20), new Size(20, 20));
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
            face,
            1.2,
            10,
            Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
            new Size(20, 20));

            foundPeople.Clear();
            // 人脸识别
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                // 圈出头像
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);

                if (trainingImages.ToArray().Length != 0)
                {
                    // TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                    // Eigen face 识别器
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                       trainingImages.ToArray(),
                       labels.ToArray(),
                       5000, // 改为2500或者3000会更准确
                       ref termCrit);

                    name = recognizer.Recognize(result);
                    foundPeople[name] = f.rect;

                    // 显示识别出的头像标签
                    //currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));                  
                }               

                NamePersons[t - 1] = name;
                NamePersons.Add("");

                // 显示探测出的头像数
                label3.Text = facesDetected[0].Length.ToString();

            }
            t = 0;

            // 头像标签字符串拼接
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
            }
            // 显示识别出的头像标签
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Text = names;
            namess = names;
            names = "";
            // Clear the list(vector) of names
            NamePersons.Clear();

        }

        /// <summary>
        /// 中文显示名字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBoxFrameGrabber_Paint(object sender, PaintEventArgs e)
        {
            Font ff = new Font("宋体", 15, FontStyle.Bold);

            if (foundPeople.Count > 0)
            {
                // 缩放
                xfactor = (float)imageBoxFrameGrabber.Width / (float)currentFrame.Bitmap.Width;
                yfactor = (float)imageBoxFrameGrabber.Height / (float)currentFrame.Bitmap.Height;

                foreach (string n in foundPeople.Keys)
                {
                    e.Graphics.DrawString(
                        n,
                        ff,
                        Brushes.LightGreen,
                        foundPeople[n].X * xfactor,
                        foundPeople[n].Y * yfactor - 30);
                }
            }

            e.Graphics.DrawString(
                       "识别人数：" + label3.Text.ToString(),
                       this.Font,
                       Brushes.Red,
                       0, 0);

            e.Graphics.DrawString(
                       foundPeople.Count.ToString(),
                       this.Font,
                       Brushes.BlanchedAlmond,
                       0, 20);
        }

        private Thread th;
        private void label4_TextChanged(object sender, EventArgs e)
        {

            th = new Thread(new ThreadStart(SpeechSound));
            th.Start();            
        }

        /// <summary>
        /// 语音播报
        /// </summary>
        private void SpeechSound()
        {
            if (string.IsNullOrEmpty(label4.Text))
            {
                return;
            }
            SpeechSynthesizer sp = new SpeechSynthesizer();
            if (namess == label4.Text)
            {
                Thread.Sleep(2500);
                if (foundPeople.Count == 1)
                    sp.SpeakAsync(label4.Text + "你好");
                if (foundPeople.Count > 1)
                    sp.SpeakAsync(label4.Text + "你们好");                             
            }
            namess = null;   
            th.Abort();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            th.Abort();
        }
    }
}
