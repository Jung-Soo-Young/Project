package ams;

import javax.swing.ImageIcon;
import javax.swing.JOptionPane;

public class AmsMain {

	public static void main(String[] args) {
		
		AmsField af = new AmsField();
		ImageIcon img = new ImageIcon("src/img/airplane.png");
		String [] arPlane = new String[5];
		String keyword = "";
		//���� ��� : �� ��ġ�� ������ ã�ư� �� �ִ� ���
		//��� ��� : �� ��ġ�� ���� ����Ǵ� ���

		String [] menu = {"�߰��ϱ�", "�˻��ϱ�", "�����ϱ�", "�����ϱ�", "��Ϻ���"};
		String [] searchMenu = {"�װ���", "�װ��� ��ȣ", "�ִ� �°���", "�����", "������"};
		String [] updateMenu = {"����� ����", "������ ����"};
		
		int index = 0;
		int choice = 0;
		
		String insertMsg = "[�߰��Ͻ� ������ �״�� �Է��ϼ���(, ����)]\n" + "�װ���, �װ����ȣ, �ִ�°���, �����, ������";
		String searchMsg = "�˻��Ͻ� �װ��縦 �Է��ϼ���\n";
		String deleteMsg = "�����Ͻ� �װ��� ��ȣ�� �Է��ϼ���\n";
		String UpdateMsg = "�����Ͻ� �װ��� ��ȣ�� �Է��ϼ���\n";
		
		while(true) {
			choice = JOptionPane.showOptionDialog(null, "", "AMS", JOptionPane.DEFAULT_OPTION, JOptionPane.PLAIN_MESSAGE, img, menu, null);
		
			if (choice == -1) break;
			
			switch(choice) {
			//�߰��ϱ� ����
			case 0:
				arPlane = JOptionPane.showInputDialog(insertMsg).split(", ");
				af.insert(arPlane);
				break;
			//�˻��ϱ� ����
			case 1:
				index = JOptionPane.showOptionDialog(null, "", "AMS", JOptionPane.DEFAULT_OPTION,
						JOptionPane.PLAIN_MESSAGE, img, searchMenu, null);
				if(index != -1) {
				keyword = JOptionPane.showInputDialog("�˻��Ͻ�" + searchMenu[index] + "��(��) �Է��ϼ���");
				JOptionPane.showMessageDialog(null, af.search(keyword, index));
				}
				break;
				
			//�����ϱ� ����
			//�����, ������
			//�װ��� ��ȣ�� �����Ҷ�
			//JOptionPane.ShowOptionDialog() ����ϱ�
			//����� ����, ������ ����
			//�װ��� ��ȣ�� �Է¹ް� �ش� �װ����� ������� �������� ����
			//������� �������� �����ϸ� ���� ����
			
			case 2:
				String planeNum = JOptionPane.showInputDialog(UpdateMsg);
				String temp = af.search(planeNum, 1);
				
				if(temp.equals("�˻� ��� ����")) {
					JOptionPane.showMessageDialog(null, "���� ����");
				}else {
					index = JOptionPane.showOptionDialog(null, "", "AMS" , JOptionPane.DEFAULT_OPTION, JOptionPane.PLAIN_MESSAGE, img, updateMenu, null);
					String newValue = JOptionPane.showInputDialog("�����Ͻ� " + updateMenu[index] + "�� �Է��ϼ���");
					af.update(planeNum, index, newValue);
				}
				break;
			//�����ϱ� ����
			case 3:
				keyword = JOptionPane.showInputDialog(deleteMsg);
				if(af.delete(keyword)) {
					JOptionPane.showMessageDialog(null, "���� �Ϸ�");
				} else {
					JOptionPane.showMessageDialog(null, "���� ����");
				}
				break;
			//��Ϻ��� ����
			case 4:
				JOptionPane.showMessageDialog(null, af.show());
				break;
			}
		}
		System.out.println(choice);
	}

}
