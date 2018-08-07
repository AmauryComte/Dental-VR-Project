using System;
using System.IO;

using MassAnimation.Adapters.PhotoAdapter;
using MassAnimation.Avatar.Entities;
using MassAnimation.Modeling;
using MassAnimation.Resources;
using MassAnimation.Resources.Entities;
using MassAnimation.Utility;

using UnityEngine;


namespace Assets.Scripts.NFEditor
{

	public class ModelConnector 
	{

		public string _frontImgPath; 

		internal ModelConnector ()
		{
		}

		internal ModelConnector (string frontalImagePath)
		{
			_frontImgPath = frontalImagePath;
		}	


		internal Animatable BuildAvatarFromFrontImage(System.Drawing.Point[] pointLocations )
		{
			Animatable anim = null;
			
			try
			{

				if ( (_frontImgPath == null) || (!File.Exists(_frontImgPath))  )
				{
					return anim;
				}

				System.Drawing.Image frontImg = System.Drawing.Image.FromFile(_frontImgPath);
				
				MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]> imgPtPair = 
					new MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]>(frontImg, pointLocations);
				
				MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]>[] mdls = 
					new MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]>[1];
				
				mdls[0]= imgPtPair;
				
				string outputPath = ResourceDirectories.TempModelDirectory;
				
				AvatarAdapter avatarAdapter = null;
				
				try
				{
					
					avatarAdapter = new AvatarAdapter("avatar",                                                 
					                                  outputPath,                                                  
					                                  mdls,
					                                  ModelDensity.HIGH,
					                                  "",
					                                  false);
				}
				catch(Exception adExp)
				{
					Debug.LogException(adExp);
				}
				
				if (avatarAdapter != null)
				{
                    int speedUp = 1;
					IAvatar avatar = avatarAdapter.Run("", EyeColor.Brown, speedUp);
					anim = avatar.ToAnimatable();
				}				
				
			}
            catch (AvatarGenerationException age)
            {
                Debug.LogException(age);
            }
			catch(UnityException exp)
			{
				Debug.LogException(exp);
			}
			
			return anim;
			
		}

		internal Animatable BuildAvatarFromFrontImage(string frontImagePath, System.Drawing.Point[] pointLocations, EyeColor eyeColor, int speedUp )
		{
			Animatable anim = null;

			try
			{

				System.Drawing.Image frontImg = System.Drawing.Image.FromFile(frontImagePath);
				
				MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]> imgPtPair = 
					new MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]>(frontImg, pointLocations);
				
				MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]>[] mdls = 
					new MassAnimation.Resources.Tuple<System.Drawing.Image, System.Drawing.Point[]>[1];
				
				mdls[0]= imgPtPair;
				
				string outputPath = ResourceDirectories.TempModelDirectory;
				
				AvatarAdapter avatarAdapter = null;
				
				try
				{
					
					avatarAdapter = new AvatarAdapter("avatar",                                                 
					                                  outputPath,                                                  
					                                  mdls,
					                                  ModelDensity.HIGH,
					                                  "",
					                                  false);
				}
				catch(Exception adExp)
				{
					Debug.LogException(adExp);
                    Debug.Log("AvatarAdapter creation failed. ");
                    throw;
				}
				
				if (avatarAdapter != null)
				{
                    IAvatar avatar = null;

                    try
                    {
                        avatar = avatarAdapter.Run("", eyeColor, speedUp);
                    }
                    catch (AvatarGenerationException agExp)
                    {
                        string fitErrorMsg = null;
                        
                        if (string.Equals(agExp.Message, "User fiducial points not properly placed.", StringComparison.InvariantCultureIgnoreCase))
                        {
                            fitErrorMsg = "Sorry, you did not position the points properly. Please follow the demo videos exactly." ;
                        }

                        throw new ApplicationException(fitErrorMsg);

                    }

                    if (avatar != null)
                    {
                        anim = avatar.ToAnimatable();
                    }
				}
				
			}
			catch(Exception exp)
			{
				Debug.LogException(exp);
                throw;
			}

			return anim;

		}


	}

}