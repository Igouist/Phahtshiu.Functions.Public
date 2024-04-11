# 打手（Phahtshiu）小助手

拍手：phah-tshiú<br/>
釋義：打手、保鑣。受人僱用，幫人打架的人<br/>
用例：做人的拍手 (Tsò lâng ê phah-tshiú.)  ＝做他人的打手

像我這麼懶的人，當然需要一堆打手幫我幹一些雜事嘛

所以這個 Phahtshiu 系列就是個給我搓小功能來玩的地方= =

~~Azure Functions 好香啊~~

---

## LineBot API 相關功能

使用 HttpTrigger 來接 LineBot 甩來的 Webhook 訊息

目前支援的指令：

- `/r`
  - `/r` 產生 1~100 之間的亂數
  - `/r 20` 產生 1~20 之間的亂數
  - `/r 蘋果 香蕉` 替多個選項各自產生 1~100 之間的亂數並排序
  - `/r 20 蘋果 香蕉` 替多個選項各自產生 1~20 之間的亂數並排序
- `/swim`
  - `/swim` 查詢預設運動中心游泳人數資訊 (預設: 松山)
  - `/swim 北投` 查詢北投運動中心游泳人數資訊

![Image](https://i.imgur.com/zlfCoAv.png)

---

## 其他速記

Q: 為什麼沒有 Domain 層<br/>
A: 這團小工具哪來的 Domain = =

Q: 為什麼要使用 Azure Functions<br/>
A: 因為我沒錢租 Server