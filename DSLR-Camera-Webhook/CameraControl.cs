using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraControl.Devices;
using CameraControl.Devices.Classes;

namespace DSLR_Camera_Webhook
{
    class CameraControl
    {
        CameraDeviceManager DeviceManager;

        public CameraControl()
        {
            DeviceManager = new CameraDeviceManager();

            DeviceManager.UseExperimentalDrivers = true;
            DeviceManager.DisableNativeDrivers = false;

            DeviceManager.CameraSelected += CameraSelected;
            DeviceManager.CameraConnected += CameraConnected;
            DeviceManager.CameraDisconnected += CameraDisconnected;

            DeviceManager.ConnectToCamera();
        }

        public void CapturePhoto()
        {
            DeviceManager.SelectedCameraDevice.CaptureInSdRam = false;
            DeviceManager.SelectedCameraDevice.CapturePhoto();
        }

        public string CurrentCamera()
        {
            return DeviceManager.SelectedCameraDevice.DisplayName;
        }

        private static void CameraSelected(ICameraDevice cameraDeviceOld, ICameraDevice cameraDeviceNew)
        {
            //Console.WriteLine(cameraDeviceNew.DisplayName + " has been selected.");
        }

        private static void CameraConnected(ICameraDevice cameraDevice)
        {
            //Console.WriteLine(cameraDevice.DisplayName + " is connected.");
        }

        private static void CameraDisconnected(ICameraDevice cameraDevice)
        {
            //Console.WriteLine(cameraDevice.DisplayName + " is disconnected.");
        }
    }
}