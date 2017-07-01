package org.cross.sms.gui;

import java.io.*;
import javax.sound.sampled.*;
import java.net.*;

/**
 * <p>Title: LianLianKan</p>
 * <p>Description: ������</p>
 * <p>Copyright: Copyright (c) 2004</p>
 * <p>Company: www.wuhantech.com</p>
 * @author ZhangJian
 * @version 1.0
 */

public class Sound
    implements Runnable {

  String currentName;
  Object currentSound;
  Thread thread;
  String[] filename = {
      "msg.wav"
      ,"send.wav"
//      , "sound/bomb.wav",
//      "sound/refresh.wav", "sound/hint.wav"
  };

  public static int MSG = 0;
  public static int SEND = 1;
//  public static int BOMB = 2;
//  public static int REFRESH = 3;
//  public static int HINT = 4;

  public Sound(int sound) {
    if (sound < 0 || sound > 4) {
      return;
    }

    URLClassLoader urlLoader = (URLClassLoader)this.getClass().getClassLoader();
    URL url = urlLoader.findResource(filename[sound]);

    try {
      currentSound = AudioSystem.getAudioInputStream(url);
    }
    catch (Exception ex1) {
      currentSound = null;
      return;
    }

    if (currentSound instanceof AudioInputStream) {
      try {
        AudioInputStream stream = (AudioInputStream) currentSound;
        AudioFormat format = stream.getFormat();

        DataLine.Info info = new DataLine.Info(
            Clip.class,
            stream.getFormat(),
            ( (int) stream.getFrameLength() *
             format.getFrameSize()));

        Clip clip = (Clip) AudioSystem.getLine(info);
        clip.open(stream);
        currentSound = clip;
      }
      catch (Exception ex) {
        currentSound = null;
        return;
      }
    }

    if (currentSound != null) {
      start();
    }
  }

  public void playSound() {
    if (currentSound instanceof Clip) {
      Clip clip = (Clip) currentSound;
      clip.start();
      try {
        thread.sleep(999);
      }
      catch (Exception e) {
      }
      while (clip.isActive() && thread != null) {
        try {
          thread.sleep(99);
        }
        catch (Exception e) {
          break;
        }
      }

      clip.stop();
      clip.close();
    }
    currentSound = null;
  }

  public void start() {
    thread = new Thread(this);
    thread.start();
  }

  public void run() {
    playSound();
    stop();
  }

  public void stop() {
    if (thread != null) {
      thread.interrupt();
    }
    thread = null;
  }
}
