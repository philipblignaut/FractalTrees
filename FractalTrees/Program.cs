using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornucopiaV2;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace FractalTrees
{
	class Program
	{
		static void Main(string[] args)
		{
			new Program().Run(args);
		}

		private void Run(string[] args)
		{
			INIHandler iniHandler = new INIHandler(@"D:\numbers\philip\hi\tree.ini");
			//iniHandler.WriteValue("Tree3", "maxLevel", "13");
			//iniHandler.WriteValue("Tree3", "trunkX", "-100");
			//iniHandler.WriteValue("Tree3", "trunkLength", "600");
			//iniHandler.WriteValue("Tree3", "trunkLineWidth", "15");
			//iniHandler.WriteValue("Tree3", "leftTurnAngle", "20");
			//iniHandler.WriteValue("Tree3", "rightTurnAngle", "80");
			//iniHandler.WriteValue("Tree3", "leftBranchRatio", "0.65");
			//iniHandler.WriteValue("Tree3", "rightBranchRatio", "0.65");
			//string[] trees = new string[]
			//{
			//  "θ1=30°, θ2=35°, k1=0.65, k2=0.75",
			//  "θ1=15°, θ2=45°, k1=k2=0.5√2",
			//  "θ1=30°, θ2=60°, k1=0.65, k2=0.75",
			//  "θ1=20°, θ2=80°, k1=k2=0.65",
			//  "θ1=10°, θ2=90°, k1=k2=0.5√2",
			//  "θ1=θ2=45°, k1=k2=0.5√2",
			//  "θ1=45°, θ2=90°, k1=0.7, k2=0.65",
			//  "θ1=45°, θ2=120°, k1=0.75, k2=0.6",
			//  "θ1=90°, θ2=345°, k1=k2=0.5√2",
			//  "θ1=θ2=90°, k1=k2=0.7",
			//  "θ1=35°, θ2=78°, k1=0.78, k2=0.77",
			//}
			//;
			//int ix = 0;
			//int i;
			//		float d1, d2;
			//float k1, k2, s;
			//string boom;
			//foreach (string tree in trees)
			//{
			//	if (tree.StartsWith("θ1=θ2="))
			//	{
			//		boom = tree.Substring(6);
			//		i = boom.IndexOf("°");
			//		d1 = float.Parse(boom.Substring(0, i));
			//		d2 = d1;
			//		i = boom.IndexOf(" ");
			//		boom = boom.Substring(i);
			//	}
			//	else
			//	{
			//		boom = tree.Substring(3);
			//		i = boom.IndexOf("°");
			//		d1 = float.Parse(boom.Substring(0, i));
			//		i = boom.IndexOf("θ2=");
			//		boom = boom.Substring(i+3);
			//		i = boom.IndexOf("°");
			//		d2 = float.Parse(boom.Substring(0, i));
			//		i = boom.IndexOf(" ");
			//		boom = boom.Substring(i);
			//	}
			//	boom = boom.Trim();
			//	if (boom.StartsWith("k1=k2="))
			//	{
			//		boom = boom.Substring(6);
			//		i = boom.IndexOf("√");
			//		if (i<0)
			//		{
			//			k1 = float.Parse(boom);
			//			k2 = k1;
			//		}
			//		else
			//		{
			//			k1 = float.Parse(boom.Substring(0, i));
			//			boom = boom.Substring(i+1);
			//			s = float.Parse(boom);
			//			k1 *= s.SqrtF();
			//			k2 = k1;
			//		}
			//	}
			//	else
			//	{
			//		boom = boom.Substring(3);
			//		i = boom.IndexOf(",");
			//		k1 = float.Parse(boom.Substring(0, i));
			//		i = boom.IndexOf("=");
			//		boom = boom.Substring(i+1);
			//		k2 = float.Parse(boom);
			//	}
			//	ix++;
			//	iniHandler.WriteValue("Tree"+ix, "maxLevel", "13");
			//	iniHandler.WriteValue("Tree"+ix, "trunkX", "-100");
			//	iniHandler.WriteValue("Tree"+ix, "trunkLength", "600");
			//	iniHandler.WriteValue("Tree"+ix, "trunkLineWidth", "15");
			//	iniHandler.WriteValue("Tree"+ix, "leftTurnAngle", d1.ToString());
			//	iniHandler.WriteValue("Tree"+ix, "rightTurnAngle", d2.ToString());
			//	iniHandler.WriteValue("Tree"+ix, "leftBranchRatio", k1.ToString());
			//	iniHandler.WriteValue("Tree"+ix, "rightBranchRatio", k2.ToString());

			//}

			//for (int tree = 1; tree < 12; tree++)
			//{
		int tree = 10;
				float maxLevel = float.Parse(iniHandler.ReadValue("Tree" + tree, "maxLevel"));
				float trunkX = float.Parse(iniHandler.ReadValue("Tree" + tree, "trunkX"));
				float trunkLength = float.Parse(iniHandler.ReadValue("Tree" + tree, "trunkLength"));
				float trunkLineWidth = float.Parse(iniHandler.ReadValue("Tree" + tree, "trunkLineWidth"));
				float leftTurnAngle = float.Parse(iniHandler.ReadValue("Tree" + tree, "leftTurnAngle")).ToRadians();
				float rightTurnAngle = float.Parse(iniHandler.ReadValue("Tree" + tree, "rightTurnAngle")).ToRadians();
				float leftBranchRatio = float.Parse(iniHandler.ReadValue("Tree" + tree, "leftBranchRatio"));
				float rightBranchRatio = float.Parse(iniHandler.ReadValue("Tree" + tree, "rightBranchRatio"));
				float level = 0;

				ColorGradientFactory cgf =
					new ColorGradientFactory
					(Color.Brown
					, Color.Brown
					, Color.DarkGreen
					, Color.Green
					, Color.LightGreen
					)
					;
				;
			using (CorImage image = new CorImage(2000, 2000, Color.White))
			{
				Point point= image.Graphics.RenderingOrigin;
				ConDeb.Print(point.ToString());
				image.BottomCartesian();
				point = image.Graphics.RenderingOrigin;
				image.Graphics.DrawRectangle(new Pen(Color.Black,20), -1000, 0, 2000, 2000);
				ConDeb.Print(point.ToString());
				PosVector trunk =
					new PosVector
					(new PointF(0,0)// (trunkX, 0)
					, trunkLength
					, 90F.ToRadians()
					)
					;
				ConDeb.Print($"{trunk.Start} {trunk.End}");
				DrawBranch
					(image
					, trunk
					, cgf.ColorAtPercent(cgf.CalculatePercent(level, maxLevel) / 100)
					, trunkLineWidth
					)
					;
				level++;
				float lineWidth = trunkLineWidth * (maxLevel - level) / maxLevel;
				CreateAndDrawBranches
					(image
					, trunk
					, level
					, maxLevel
					, lineWidth
					, leftTurnAngle
					, rightTurnAngle
					, leftBranchRatio
					, rightBranchRatio
					, cgf
					)
					;
				//
				//cgf.ColorAtPercent(cgf.CalculatePercent(level, maxLevels))
				image
					.Save
					($@"d:\numbers\philip\hi\tree-{tree}-"
					+ $@"{leftBranchRatio:#.###}-{rightBranchRatio:#.###}-"
					+ $@"{leftTurnAngle:#.###}-{rightTurnAngle:#.###}"
					+ ".png"
					, ImageFormat.Png
					)
					;
			}
			//}
		}

		public void CreateAndDrawBranches
			(CorImage image
			, PosVector parent
			, float level
			, float maxLevel
			, float lineWidth
			, float leftTurnAngle
			, float rightTurnAngle
			, float leftBranchRatio
			, float rightBranchRatio
			, ColorGradientFactory cgf
			)
		{
			PosVector left =
				new PosVector
				(parent.End
				, parent.Length * leftBranchRatio
				, parent.AngleRadians + leftTurnAngle
				)
				;
			PosVector right =
				new PosVector
				(parent.End
				, parent.Length * rightBranchRatio
				, parent.AngleRadians - rightTurnAngle
				)
				;
			float colorPercentage = level / maxLevel * 100;
			Color color = cgf.ColorAtPercent(colorPercentage);
			DrawBranch
				(image
				, left
				, color
				, lineWidth
				)
			;
			DrawBranch
				(image
				, right
				, color
				, lineWidth
				)
			;
			if (level <= maxLevel)
			{
				lineWidth = lineWidth * (maxLevel- level) / maxLevel;
				level++;
				CreateAndDrawBranches
					(image
					, left
					, level
					, maxLevel
					, lineWidth
					, leftTurnAngle
					, rightTurnAngle
					, leftBranchRatio
					, rightBranchRatio
					, cgf
					)
					;
				CreateAndDrawBranches
					(image
					, right
					, level
					, maxLevel
					, lineWidth
					, leftTurnAngle
					, rightTurnAngle
					, leftBranchRatio
					, rightBranchRatio
					, cgf
					)
					;
			}
		}

		public void DrawBranch
			(CorImage image
			,PosVector branch
			, Color color
			, float lineWidth
			)
		{

			image
			.DrawLine
			(color
			, lineWidth
			, branch.Start
			, branch.End
			)
			;
		}
	}
	//image.FillRectangleSolid(Color.Plum, 10, 150, 800, 400);
	//image.DrawArc(Color.Black, 4, 0, 0, 180, 180, 0, 180);
	//image
	//	.Graphics
	//	.DrawEllipse
	//	(Pens.Red
	//	, new RectangleF
	//		(100, 100, 400, 400
	//		)
	//	)
	//	;
}
