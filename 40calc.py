from tkinter import *
from tkinter import ttk

def gethits(natk, bws, trt=False, mod=0, rro=False, ovw=False, crt=False):
     ##natk-number of attacks-int
     ##bws-ballistic/weapon skill-int
     ##trt-torrent-bool
     ##mod-modifier-int
     ##rr0-reroll ones-bool
     ##ovw-overwatch-bool
     ##crt-get crits-bool
     if trt:
         print(str(natk)+" hits")
         return natk
     ##torrent bypasses hitcheck
     crits=natk/6+int(rro)*natk/36
     ##crits=natural 6s
     ews=max(bws-mod, 2)
     ##minimum possible weapon skill is 2
     if ovw:
         ews=6
     ##overwatch forces weapon skill=6
     hitprob=max((4-(ews-2))/6, 0)*(6+rro)/6
     ##hitprob:probability of successful hit for a single attack
     nhits=natk*hitprob
     totalhits=nhits+crits
     print(str(round(totalhits,3))+" hits, of which "+str(round(crits,3))+" are critical hits")
     if crt:
         return [totalhits,crits]
     else:
         return totalhits 

def getwounds(nht, wsr, tug, ams, pic, dmg, crt=0, ivs=7, twl=False, mod=0, cov=False, ant=6, fnp=7, dev=False, let=False):
     ##nht:number of hits-int
     ##crt: number of critical hits-int
     ##wsr:weapon strength-int
     ##tug: target toughness-int
     ##ams: armour save-int
     ##ivs: invulnerable save-int
     ##pic: armour piercing-int
     ##dmg: damage-int
     ##twl: twin-linked-bool
     ##mod: wound modifier-int
     ##cov: cover-bool
     ##ant: anti-int
     ##fnp: feel no pain-int
     ##dev: devastating wounds-bool
     ##let: lethal hits-bool
     wratio=wsr/tug
     if wratio==1:
         basewthreshold=4
     elif wratio<1 and wratio>(1/2):
         basewthreshold=5
     elif wratio<=(1/2):
         basewthreshold=6
     elif wratio>1 and wratio<2:
         basewthreshold=3
     elif wratio>=2:
         basewthreshold=2
     ##basewthreshold: base wound threshold
     effwthreshold=max(min((basewthreshold-mod), ant),2)
     ##effwthreshold: effective wound threshold-roll needed to score a potential wound
     ##effective wound threshold cannot be less than 2 or greater than 6
     ehit=nht-let*crt
     ##ehit: effective hits where wound roll is calculated
     basefailw=ehit*(effwthreshold-1)/6
     ##basefailw: probable number of failures to wound before twin-linked
     crits = (ehit+twl*basefailw)*(7-ant)/6
     ##crits: expected number of critical wounds
     wounds = (ehit+twl*basefailw)*max(((ant-2)-(effwthreshold-2))/6, 0)+let*crt
     ##wounds: expected number of normal wounds
     totalwounds=crits+wounds
     print(str(round(totalwounds, 3))+" successful wounds, of which "+str(round(crits, 3))+" critical wounds")
     if pic==0 and ams<=3:
          cov=False
     ##ignore cover if armour save is better than 3 and piercing is 0
     effamr=min((ams+pic-cov), ivs)
     ##effamr: effective armour save-cannot be worse than invulnerable save
     saveprob=max((7-effamr)/6, 0)
     ##saveprob: probability of successful armour save
     potwounds=dev*crits+(wounds+(not dev)*crits)*(1-saveprob)
     ##potwounds: expected number of wounding attacks
     print(str(round(potwounds,3))+" wounding attacks")
     truewounds=potwounds*(1-(max((7-fnp)/6, 0)))*dmg
     print("average number of wounds is "+str(round(truewounds,3)))
     return truewounds
     
def getoutcome(natk, bws, wsr, tug, ams, pic, dmg, trt=False, hmod=0, rro=False, ovw=False, ivs=7, twl=False, wmod=0, cov=False, ant=6, fnp=7, dev=False, let=False):
     hits=gethits(natk=natk, bws=bws, trt=trt, mod=hmod, rro=rro, ovw=ovw, crt=let)
     if let:
          crits=hits[1]
          hits=hits[0]
     else:
          crits=0
     ##checks to see if gethits will return a list or a single float
     return getwounds(nht=hits, wsr=wsr, tug=tug, ams=ams, pic=pic, dmg=dmg, crt=crits, ivs=ivs, twl=twl, mod=wmod, cov=cov, ant=ant, fnp=fnp, dev=dev, let=let)

def calculate():
     avgwounds.set(round(getoutcome(natk=natk.get(), bws=bws.get(), wsr=wsr.get(), tug=tug.get(), pic=pic.get(), dmg=dmg.get(), ams=ams.get(),
                        ivs=ivs.get(), fnp=fnp.get(), ant=ant.get(),
                        hmod=hmod.get(), wmod=wmod.get(),
                        trt=torrent.get(), rro=rro.get(), ovw=ovw.get(), twl=twl.get(), cov=cov.get(), dev=dev.get(), let=let.get()), 3))
root = Tk()
root.title("Attack Calculator")

mainframe = ttk.Frame(root, padding="3 3 12 12")
mainframe.grid(column=0, row=0, sticky=(N, W, E, S))
root.columnconfigure(0, weight=1)
root.rowconfigure(0, weight=1)

natk = IntVar()
natk_entry = ttk.Entry(mainframe, width=7, textvariable=natk)
natk_entry.grid(column=2, row=2, sticky=(W, E))
natk_entry.insert(END, 1)
ttk.Label(mainframe, text="attacks").grid(column=2, row=1, sticky=W)


bws = IntVar()
bws_entry = ttk.Entry(mainframe, width=7, textvariable=bws)
bws_entry.grid(column=3, row=2, sticky=(W, E))
bws_entry.insert(END, 4)
ttk.Label(mainframe, text="ballistic/weapon skill").grid(column=3, row=1, sticky=W)


wsr = IntVar()
wsr_entry = ttk.Entry(mainframe, width=7, textvariable=wsr)
wsr_entry.grid(column=4, row=2, sticky=(W, E))
wsr_entry.insert(END,2)
ttk.Label(mainframe, text="weapon strength").grid(column=4, row=1, sticky=W)


pic = IntVar()
pic_entry = ttk.Entry(mainframe, width=7, textvariable=pic)
pic_entry.grid(column=5, row=2, sticky=(W, E))
ttk.Label(mainframe, text="armour piercing value (positive)").grid(column=5, row=1, sticky=W)


dmg = IntVar()
dmg_entry = ttk.Entry(mainframe, width=7, textvariable=dmg)
dmg_entry.grid(column=6, row=2, sticky=(W, E))
dmg_entry.insert(END, 1)
ttk.Label(mainframe, text="weapon damage").grid(column=6, row=1, sticky=W)


tug = IntVar()
tug_entry = ttk.Entry(mainframe, width=7, textvariable=tug)
tug_entry.grid(column=2, row=4, sticky=(W, E))
tug_entry.insert(END, 2)
ttk.Label(mainframe, text="target toughness").grid(column=2, row=3, sticky=W)


ams = IntVar()
ams_entry = ttk.Entry(mainframe, width=7, textvariable=ams)
ams_entry.grid(column=3, row=4, sticky=(W, E))
ams_entry.insert(END, 4)
ttk.Label(mainframe, text="target armour save").grid(column=3, row=3, sticky=W)


ivs = IntVar()
ivs_entry = ttk.Entry(mainframe, width=7, textvariable=ivs)
ivs_entry.grid(column=4, row=4, sticky=(W, E))
ivs_entry.insert(END, 7)
ttk.Label(mainframe, text="target invulnerable save (optional)").grid(column=4, row=3, sticky=W)


fnp = IntVar()
fnp_entry = ttk.Entry(mainframe, width=7, textvariable=fnp)
fnp_entry.grid(column=5, row=4, sticky=(W, E))
fnp_entry.insert(END, 7)
ttk.Label(mainframe, text="target feel no pain (optional)").grid(column=5, row=3, sticky=W)


ant = IntVar()
ant_entry = ttk.Entry(mainframe, width=7, textvariable=ant)
ant_entry.grid(column=6, row=4, sticky=(W, E))
ant_entry.insert(END, 6)
ttk.Label(mainframe, text="anti-trait value (optional)").grid(column=6, row=3, sticky=W)


hmod = IntVar()
hmod_entry = ttk.Entry(mainframe, width=7, textvariable=hmod)
hmod_entry.grid(column=2, row=6, sticky=(W, E))
ttk.Label(mainframe, text="to-hit modifier (optional)").grid(column=2, row=5, sticky=W)


wmod = IntVar()
wmod_entry = ttk.Entry(mainframe, width=7, textvariable=wmod)
wmod_entry.grid(column=3, row=6, sticky=(W, E))
ttk.Label(mainframe, text="to-wound modifier (optional)").grid(column=3, row=5, sticky=W)


torrent = BooleanVar()
torrentc = ttk.Checkbutton(mainframe, text='Torrent', 
	    variable=torrent,
	    onvalue=True, offvalue=False)
torrentc.grid(column=1, row=8, sticky=(W,E))


rro = BooleanVar()
rroc = ttk.Checkbutton(mainframe, text='Reroll 1s to hit', 
	    variable=rro,
	    onvalue=True, offvalue=False)
rroc.grid(column=2, row=8, sticky=(W,E))


ovw = BooleanVar()
ovwc = ttk.Checkbutton(mainframe, text='Overwatch', 
	    variable=ovw,
	    onvalue=True, offvalue=False)
ovwc.grid(column=3, row=8, sticky=(W,E))


twl = BooleanVar()
twlc = ttk.Checkbutton(mainframe, text='Twin-linked', 
	    variable=twl,
	    onvalue=True, offvalue=False)
twlc.grid(column=4, row=8, sticky=(W,E))


cov = BooleanVar()
covc = ttk.Checkbutton(mainframe, text='Cover', 
	    variable=cov,
	    onvalue=True, offvalue=False)
covc.grid(column=5, row=8, sticky=(W,E))


dev = BooleanVar()
devc = ttk.Checkbutton(mainframe, text='Devastating wounds', 
	    variable=dev,
	    onvalue=True, offvalue=False)
devc.grid(column=6, row=8, sticky=(W,E))


let = BooleanVar()
letc = ttk.Checkbutton(mainframe, text='Lethal hits', 
	    variable=let,
	    onvalue=True, offvalue=False)
letc.grid(column=7, row=8, sticky=(W,E))



avgwounds = StringVar()
ttk.Label(mainframe, textvariable=avgwounds).grid(column=6, row=6, sticky=(W, E))
ttk.Label(mainframe, text="average wounds").grid(column=5, row=6, sticky=(W, E))
ttk.Button(mainframe, text="Calculate", command=calculate).grid(column=5, row=5, sticky=W)


for child in mainframe.winfo_children(): 
    child.grid_configure(padx=5, pady=5)

#feet_entry.focus()
root.bind("<Return>", calculate)

root.mainloop()
