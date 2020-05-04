/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package src.bsu.fpmi.educational_practice;


/**
 *
 * @author Dmitry Rogozenko,
 * Famcs 2020
 */
public interface AnswerListener extends java.util.EventListener {
    public void Result(AnswerEvent e);
}
