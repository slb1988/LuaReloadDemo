
����һ��lua��ͣ���ȸ��µ�Demo��ȫ��ʹ��һ��LuaState����R��reload���нű�

1. ���� ����ӵ�panel �󶨶�Ӧ��lua�ű���������ģ����
2. Ȼ����LuaBindingManager�������Ҫִ�нű����������

��ͬ���֮�䣬����ͨ�� "ģ����.������" �ķ�ʽ���з���

���ĵ�����������ͨ�����ַ�ʽ���� �����ռ� �ĸ���
local modname = "bbb"
local M = {}
_G[modname] = M
package.loaded[modname] = M
setmetatable(M, {__index = _G})
setfenv(1, M)

����һ�����Σ�����԰������˼·��ӵ��Լ�����Ŀ�����

have fun!