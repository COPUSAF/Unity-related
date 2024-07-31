namespace KMP算法
{
    /*
     * 力扣编号28
     * 考察KMP算法
     * 
     * 注意：
     * 前缀为不包含末字母的字符串
     * 后缀为不包含首字母的字符串
     */
    public class Solution
    {
        public int StrStr(string haystack, string needle)
        {
            if(haystack.Length == 0) 
                return 0;
            int[] next = new int[needle.Length];
            getNext(next,needle);

            int j = 0; //模式串的下标
            for(int i = 0; i < haystack.Length; i++)
            {
                while(j > 0 && needle[j] != haystack[i]) //若j下标回溯后依旧不匹配，则再次回溯
                {
                    j = next[j - 1];
                }
                if (needle[j] == haystack[i])  //匹配成功持续推进
                {
                    j++;
                }
                if (j == needle.Length) //完全匹配，返回下标
                    return i - needle.Length + 1;
            }
            return -1;
        }
        public void getNext(int[] next,string str)
        {
            int i = 0;//前缀末尾
            int j = 1;//后缀末尾
            next[0] = 0;
            for(;j < str.Length;j++)//j作为后缀末尾，也同时next数组当前下标，i作为前缀末尾，也代表了前缀的当前长度
            {
                while (i > 0 && str[i] != str[j])
                {
                    i = next[i-1];
                }
                if (str[i] == str[j])
                {
                    i++;
                }
                next[j] = i;
            }
            /*注意点，找寻最长相等前后缀，前缀的末尾一定与后缀的末尾相同
             * 所以while中要一直使str[i]和str[j]相同
             * 而不相同，就同样根据kmp算法思想，当前字母不匹配，则从前一个匹配成功字母对应的最长相等前后缀的下标处重新匹配
             * 
             * 曾错点：例如aabaaaa，按理来说最后两个a都相等，那么最长相等前后缀理应为6
             * 纠正：实际运行中，前缀末尾i并非每次循环都紧贴j跟着走，每当不匹配i都会回溯，参考代码随想录next数组获取图
            */
        }
    }
}
