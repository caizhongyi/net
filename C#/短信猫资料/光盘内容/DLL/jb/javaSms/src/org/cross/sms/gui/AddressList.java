package org.cross.sms.gui;
import java.util.*;
import java.io.*;
public class AddressList extends  Vector{
    public AddressList() {
        super();
    }

    public String getPhone(int i){
        return (String)get(i);
    }

    public void init(){
        File f = new File("address");
        if(!f.exists()) return;
        BufferedReader fr = null;
        try{
            fr = new BufferedReader(new FileReader(f));
            String add = null;
            while((add = fr.readLine())!=null){
                if(add.equals("")) continue;
                add(add);
            }
        }catch(Exception ex){

        }finally{
            try {
                 fr.close();
            } catch (Exception ex) {

            }
        }
    }

    public void save(){
        File f = new File("address");
        if(f.exists()) f.delete();
        PrintStream out = null;
        try {
            out = new PrintStream(new FileOutputStream("address"));
            for(int i=0;i<size();i++){
                out.println(this.get(i));
            }
            out.flush();
        } catch (Exception ex) {
            ex.printStackTrace();
        }finally{
            try {
                 out.close();
            } catch (Exception ex) {

            }
        }


    }
    public static void main(String[] args) {
        AddressList addresslist = new AddressList();
    }
}
