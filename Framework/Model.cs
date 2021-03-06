﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RealSense
{

    public class Model
    {
        // Reference to globally used SenseManager
        private PXCMSenseManager senseManager;
        private PXCMFaceModule face;
        private PXCMFaceData faceData;
        private PXCMFaceConfiguration faceConfig;
        public PXCMFaceData.Face faceAktuell;
        private PXCMFaceData.ExpressionsData edata;
        private PXCMHandModule hand;
        private PXCMHandData handData;

        private List<RSModule> modules;
        private int width;
        private int height;
        private int framerate;

        public Model()
        {
            width = 640;
            height = 480;
            framerate = 30;
            senseManager = PXCMSenseManager.CreateInstance();
            senseManager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, width, height, framerate);
            // Enable Face detection
            senseManager.EnableFace();
            senseManager.EnableHand();
            senseManager.Init();

            face = senseManager.QueryFace();
            faceConfig = face.CreateActiveConfiguration();
            faceConfig.SetTrackingMode(PXCMFaceConfiguration.TrackingModeType.FACE_MODE_COLOR);
            faceConfig.detection.isEnabled = true;
            faceConfig.QueryExpressions();
            PXCMFaceConfiguration.ExpressionsConfiguration expc = faceConfig.QueryExpressions();
            expc.Enable();
            expc.EnableAllExpressions();
            faceConfig.ApplyChanges();
            faceConfig.Update();

            //faceData = face.CreateOutput();
            //faceData.Update();

            hand = senseManager.QueryHand();
            PXCMHandConfiguration config = hand.CreateActiveConfiguration();
            config.SetTrackingMode(PXCMHandData.TrackingModeType.TRACKING_MODE_FULL_HAND);
            config.ApplyChanges();
            config.Update();
            //handData = hand.CreateOutput();
            //handData.Update();

            modules = new List<RSModule>();
        }

        public void AddModule(RSModule m)
        {
            modules.Add(m);
        }

        public List<RSModule> Modules
        {
            get { return modules; }
        }

        public PXCMSenseManager SenseManager
        {
            get { return senseManager; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public PXCMFaceModule Face
        {
            get { return face; }
        }

        public PXCMFaceData FaceData
        {
            get { return faceData; }
            set { faceData = value; }
        }

        public PXCMFaceData.Face FaceAktuell
        {
            get { return faceAktuell; }
            set { faceAktuell = value; }
        }

        public PXCMFaceData.ExpressionsData Edata
        {
            get { return edata; }
            set { edata = value; }
        }

        public PXCMHandModule Hand
        {
            get { return hand; }
        }

        public PXCMHandData HandData
        {
            get { return handData; }
            set { handData = value; }
        }
    }
}