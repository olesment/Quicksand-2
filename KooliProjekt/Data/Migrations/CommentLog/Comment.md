18.10 16:33
Rea estate klassis panin user user selleks, et saaks siis real estated siduda konkreetse useriga. Probleem oli selles, et sellega läks katki CRUD. 
kuna sellega ei saanud creatile ja editile kaasa anda userit, samas see oli reuired (sellel polnud?) Kui selle välja kommenteerisin ja rebuidisin
siis hakkas nagu tööle. 

Imelik miks SQL mingit errorit ei visanud siis enne, required field oli ju tühi. 

Nüüd selles real estate klassis peaks tegema nii, et sinna saab lisada küll, aga kustutada sealt otse ei saa. See peaks käima läbi transactioni. 