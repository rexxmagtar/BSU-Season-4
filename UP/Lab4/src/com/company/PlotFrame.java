package com.company;

import javax.print.attribute.HashPrintRequestAttributeSet;
import javax.print.attribute.PrintRequestAttributeSet;
import javax.print.attribute.standard.OrientationRequested;
import javax.print.attribute.standard.Sides;
import javax.swing.*;
import java.awt.*;
import java.awt.print.PageFormat;
import java.awt.print.Printable;
import java.awt.print.PrinterException;
import java.awt.print.PrinterJob;
import java.io.*;
import java.util.ArrayList;


public class PlotFrame extends JFrame implements Printable {

    private PlotFrame() {
        initComponents();
        this.setTitle("04 - astroid");
        this.setSize(400, 470);
        File f = new File("src");
        if (f.exists()) {
            System.out.println(f.getAbsolutePath());
        }
        textLines = initTextLines(new File("C:/BSU-Season-4/UP/Lab4/src/com/company/AstroidShape.java"));
    }


    private void initComponents() {

        JPanel jPanel1 = new JPanel() {

        };

    }

    public void PrintRequest() {//GEN-FIRST:event_jMenuItem1ActionPerformed
        PrinterJob job = PrinterJob.getPrinterJob();
        PrintRequestAttributeSet printRequestAttributeSet = new HashPrintRequestAttributeSet();
        printRequestAttributeSet.add(Sides.DUPLEX);
        printRequestAttributeSet.add(OrientationRequested.LANDSCAPE);

        job.setPrintable(this);
        boolean ok = job.printDialog(printRequestAttributeSet);
        if (ok) {
            try {
                job.print(printRequestAttributeSet);
            } catch (PrinterException ex) {
                System.err.print(ex);
            }
        }

    }


    public static void main(String args[]) throws ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {

        new PlotFrame().PrintRequest();
    }

    private String[] textLines;
    private int[] pageBreaks;

    @Override
    public int print(Graphics graphics, PageFormat pageFormat, int pageIndex) throws PrinterException {
        int y = 0;
        Font font = new Font("Serif", Font.PLAIN, 10);
        FontMetrics metrics = graphics.getFontMetrics(font);
        //int lineHeight = metrics.getHeight();
        int lineHeight = 12;
        if (pageIndex == 0) {
//            BufferedImage bufferedImageAll = new BufferedImage(this.getWidth(), this.getHeight(),
//                    BufferedImage.TYPE_INT_RGB);
//            Graphics2D graphics2DForImage = bufferedImageAll.createGraphics();
//            this.printAll(graphics2DForImage);
//
//            double scale = pageFormat.getWidth() / this.getWidth();
//            int newWidth = (int) (this.getWidth() * scale / 2.5);
//            int newHeight = (int) (this.getHeight() * scale / 2.5);
//
//            Image scaledImage = bufferedImageAll.getScaledInstance(newWidth, newHeight, Image.SCALE_SMOOTH);
//            graphics.drawImage(scaledImage,
//                    (int) (pageFormat.getImageableWidth() / 2 - 75), //подгод для центрирования по горизонтали
//                    (int) (pageFormat.getImageableHeight() / 40), null);

            Graphics2D graphics2D = (Graphics2D)graphics;
            graphics2D.setStroke(new MyStroke());
            graphics2D.draw(new AstroidShape( (int) pageFormat.getImageableX() +(int) pageFormat.getImageableWidth() / 2 ,
                    (int) pageFormat.getImageableY() +(int) (pageFormat.getImageableHeight() *0.45f) ));
            graphics.drawString("Astroid", (int) pageFormat.getImageableX() +(int) pageFormat.getImageableWidth() / 2 ,
                    (int) pageFormat.getImageableY() +(int) (pageFormat.getImageableHeight() *0.95f) );

return PAGE_EXISTS;
        }

        if (pageBreaks == null) {
            System.out.println(textLines.length);
            int linesPerPage = (int) (pageFormat.getImageableHeight() / lineHeight);
            int numBreaks = (textLines.length - 1) / linesPerPage + 1;
            System.out.println(numBreaks);
            pageBreaks = new int[numBreaks];
            for (int b = 0; b < numBreaks; b++) {
                pageBreaks[b] = b * linesPerPage;
            }
        }

        if (pageIndex > pageBreaks.length) {
            return NO_SUCH_PAGE;
        }

        Graphics2D g2D = (Graphics2D) graphics;
        if(pageIndex==0){
            g2D.translate((int) pageFormat.getImageableX() +(int) pageFormat.getImageableWidth() / 2 ,
                    (int) pageFormat.getImageableY() +60+(int) (pageFormat.getImageableHeight() *0.25f));
        }
        else {
            g2D.translate(pageFormat.getImageableX(), pageFormat.getImageableY());
        }
        //int start = (pageIndex == 1) ? 0 : pageBreaks[pageIndex - 1];
        int start = pageBreaks[pageIndex - 1];
        int end = (pageIndex == pageBreaks.length) ? textLines.length : pageBreaks[pageIndex];
        for (int line = start; line < end; line++) {
            y += lineHeight;
            System.out.println(y);
            graphics.drawString(textLines[line], 0, y);
        }
        return PAGE_EXISTS;
    }

    private String[] initTextLines(File file) {
        ArrayList<String> result = new ArrayList<>();

        try (BufferedReader reader = new BufferedReader(new FileReader(file))) {
            String line;
            while ((line = reader.readLine()) != null) {
                result.add(line);
            }
        } catch (FileNotFoundException ex) {
            System.out.println("file not found");
            return null;
        } catch (IOException e) {
            System.err.println(e.getMessage());
        }
        return result.toArray(new String[result.size()]);
    }
}