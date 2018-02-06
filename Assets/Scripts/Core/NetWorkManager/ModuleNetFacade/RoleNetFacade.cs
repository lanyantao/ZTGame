﻿using gprotocol;
namespace com.game.client
{
	namespace network.facade
	{
		[NetFacadeAttribute(Module.role)]
		public class RoleNetFacade
		{
			[NetCommandAttribute(Command.role_info)]
			private void OnReceive_Role_Info(int code, role_info_s2c vo)
			{
				string info = string.Empty;
				info += "Id:" + vo.role.id +", ";
				info += "Name:" + vo.role.name +", ";
				info += "Job:" + vo.role.job +", ";
				info += "Lv.:" + vo.role.level +", ";
				info += "Rank:" + vo.role.rank +", ";
				info += "Sex:" + vo.role.sex;
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_Info]角色信息:" + info);
			
			}

			[NetCommandAttribute(Command.role_exp_inc)]
			private void OnReceive_Role_EXP_Inc(int code, role_exp_inc_s2c vo)
			{
				string Str = vo.has_world_exp == 0 ? "没有" : "有";
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]获得经验:" + Str + "世界等级加成, exp" + vo.inc);
			}

			[NetCommandAttribute(Command.role_gongxun)]
			private void OnReceive_Role_GongXun(int code, role_gongxun_s2c vo)
			{
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]当前功勋:" + vo.rank);
			}

			[NetCommandAttribute(Command.role_vigour_info)]
			private void OnReceive_Role_Vigour_Info(int code, role_vigour_info_s2c vo)
			{
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]体力信息:" + vo.vigour);
			}





			[NetCommandAttribute(Command.role_bc_info)]
			private void OnReceive_Role_BC_Info(int code, role_bc_info_s2c vo)
			{
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]推帧:[" + vo.role_id + ":" + vo.data + "]");
			}

			[NetCommandAttribute(Command.role_attr)]
			private void OnReceive_Role_aasdfsdf(int code, role_attr_s2c vo){
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]属性更新:当前生命值" + vo.attr.hp_cur + "...");
			}


			[NetCommandAttribute(Command.role_update_fight_point)]
			private void OnReceive_Role_aasdfsdf(int code, role_update_fight_point_s2c vo){
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]战力更新:" + vo.fight_point);
			}


			[NetCommandAttribute(Command.role_fortune)]
			private void OnReceive_Role_aasdfsdf(int code, role_fortune_s2c vo){
				UnityEngine.Debug.Log ("[" + System.DateTime.Now + "]" + "[OnReceive_Role_BC_Info]财富更新:Scoin:" + vo.coin + ", coupon:" + vo.coupon + ", gold:" + vo.gold + ", silver:" + vo.silver);
			}







		}
	}
}