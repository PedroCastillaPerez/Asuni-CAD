#ifndef _TABLERO_HPP_
#define _TABLERO_HPP_

enum TableroTile
{
	TABLERO_PARED = 1,
	TABLERO_VACIO,
	TABLERO_COCO = 17

};

class Tablero
{
	public:
		Tablero();
		~Tablero();
		void Dibujar();
		bool EsPared(int posX, int posY);
		bool TodosCocosComidos();
		bool ComerCoco(int posX, int posY);
		void PonerPared(int posX, int posY);
		void QuitarPared(int posX, int posY);
		const int tilemap = 0;
	private:
		void PonerLineaHorizontalParedes(int x, int y, int longitud);
		void PonerLineaVerticalParedes(int x, int y, int longitud);
		int numCocos;
};

#endif