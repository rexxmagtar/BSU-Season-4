__kernel void Encrypt(__global char* bytes, __global char* s)
{
	int index = get_global_id(0);
	 
	for (int i = 0; i < 8; i++)
	{
		// ������ 1 ��� 0 i ��� � s[index]
		// ��������� ��� � ��������� ��� ���������������� ����� � bytes
		if (s[index] & (1 << i))
			bytes[index * 8 + i] |= 1; 
		else
			bytes[index * 8 + i] &= 254;
	}
}