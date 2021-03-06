#/usr/bin/env python
#coding=utf-8
import sys  
reload(sys)  
sys.setdefaultencoding('utf8') 

import os
import xlrd
g_root_path = os.getcwd()

def ToArrTab(value,isStr):
	arr = value.split('^')
	res = '{'
	for i in range(0, len(arr)):
		if isStr:
			res = res +('\'%s\',' % arr[i])
		else:
			res = res + '%d,' % int(arr[i])
	res = res +'}'
	#print res
	return res

def writeLua(fname):
	print fname
	typeList = ["int", "string", "float", "[int]","[string]"]
	clientFlag = ['k','a','c']
 	try:
 		# excel data dict
 		excel_data_dict = {}
 		#col name type list
 		col_name_list = []
 		#col val type list
 		col_val_type_list = []

 		col_client_flag_list = []

 		workbook = xlrd.open_workbook(fname)
 		booksheet = workbook.sheets()[0]

 		# 遍历第2行的所有列 保存读取类型
 		for col in range(0, booksheet.ncols):
 			cell = booksheet.cell(1, col)
 			col_client_flag_list.append(str(cell.value))
 			#print cell.value
 			assert cell.ctype == 1, "found a invalid col val in col [%d] !~" % (col)

 		# 遍历第3行的所有列 保存数据类型
 		for col in range(0, booksheet.ncols):
 			cell = booksheet.cell(2, col)
 			valueType = str(cell.value)
			if not valueType in typeList:
				valueType = 'int'
 			col_val_type_list.append(str(cell.value))
 			assert cell.ctype == 1, "found a invalid col val in col [%d] !~" % (col)
 		# 遍历第4行的所有列 保存字段名
 		for col in range(0, booksheet.ncols):
 			cell = booksheet.cell(3, col)
 			col_name_list.append(str(cell.value))
 			assert cell.ctype == 1, "found a invalid col name in col [%d] !~" % (col)
 		# 剔除表头、字段名和字段类型所在行 从第四行开始遍历 构造行数据
 		for row in range(4, booksheet.nrows):
 			# 保存数据索引 默认第一列为id
 			cell_id = booksheet.cell(row, 0)
 			assert cell_id.ctype == 2, "found a invalid id in row [%d] !~" % (row)
 			# 检查id的唯一性
 			if cell_id.value in excel_data_dict:
 				print('[warning] duplicated data id: "%d", all previous value will be ignored!~' % (cell_id.value))
			
			# row data list
			row_data_list = []

			# 保存每一行的所有数据
			for col in range(0, booksheet.ncols):
				cell = booksheet.cell(row, col)
				k = col_name_list[col]
				cell_val_type = col_val_type_list[col]
				# ignored the string that start with '_'
				if str(k).startswith('_'):
					continue
				# 根据字段类型去调整数值 如果为空值 依据字段类型 填上默认值
				if cell_val_type == 'string':
					if cell.ctype == 0:
						v = '\'\''
					else:
						v = '\'%s\'' % (cell.value)
				elif cell_val_type == 'int':
					if cell.ctype == 0:
						v = -1
					else:
						v = int(cell.value)
				elif cell_val_type == 'float':
					if cell.ctype == 0:
						v = -1
					else:
						v = float(cell.value)
				elif cell_val_type == '[int]':
					if cell.ctype == 0:
						v = '{}'
					else:
						v = ToArrTab(cell.value,False)
				elif cell_val_type == '[string]':
					if cell.ctype == 0:
						v = '{}'
					else:
						v = ToArrTab(cell.value,True)
				else:
					if cell.ctype == 0:
						v = -1
					else:
						v = int(cell.value)
				# 加入列表
				row_data_list.append([k, v,col_client_flag_list[col]])
			# 保存id 和 row data
			excel_data_dict[cell_id.value] = row_data_list

 		# # export to lua file
 		lua_export_file = open('../Assets/LuaScript/ALuaConfig/' + booksheet.name + '.txt', 'w')
 		lua_export_file.write('%s = {\n' % booksheet.name)
 		# 遍历excel数据字典 按格式写入
 		for k, v in excel_data_dict.items():
 			lua_export_file.write('[%d] = {' % k)
 			for row_data in v:
 				if row_data[2] in clientFlag:
 					lua_export_file.write(' {0} = {1},'.format(row_data[0], row_data[1]))
 			lua_export_file.write('  },\n')

 		lua_export_file.write('}\n')
 		lua_export_file.close()
	except Exception,e:
		print str(e)

def main():
    #dir = raw_input('please input the path:')
    filenames = os.listdir(g_root_path)
    for fname in filenames :
        (filename,extension) = os.path.splitext(fname)
        if fname.find('~$') == -1 and (extension == ".xlsx" or extension == ".xls"):
    		writeLua(fname)


if __name__ == '__main__':
    main()
    os.system('pause')
