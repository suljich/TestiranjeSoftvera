import matplotlib.pyplot as plt
import pandas as pd

data = {
    'Greške': ['Struktura programskog rješenja', 'Dizajn programskog rješenja', 'Petlje i grananja programskog rješenja', 'Memorijskie operacija programskog rješenja', 'Dokumentacija programskog rješenja'],
    'Broj ponavljanja': [1, 1, 1, 1, 3]
}

df = pd.DataFrame(data)

df = df.sort_values(by='Broj ponavljanja', ascending=False)

df['Udio'] = (df['Broj ponavljanja'] / df['Broj ponavljanja'].sum()).cumsum()

plt.figure(figsize=(10, 5))
plt.bar(df['Greške'], df['Broj ponavljanja'], color='skyblue')
plt.xticks(rotation=45, ha='right')
plt.ylabel('Broj ponavljanja')
plt.twinx()
plt.plot(df['Greške'], df['Udio'], color='red', marker='o')
plt.ylabel('Udio (%)')

threshold = 0.8
plt.axhline(threshold, color='gray', linestyle='--', linewidth=1)
plt.annotate(f'80% udio na {threshold:.0%}', xy=(df['Greške'][0], threshold), xytext=(10, 10),
             textcoords='offset points', arrowprops=dict(arrowstyle="->"))

plt.title('Pareto dijagram grešaka')
plt.tight_layout()
plt.show()

plt.figure(figsize=(10, 5))
plt.bar(df['Greške'], df['Broj ponavljanja'], color='lightgreen')
plt.xticks(rotation=45, ha='right')
plt.ylabel('Broj ponavljanja')
plt.title('Histogram grešaka')
plt.tight_layout()
plt.show()