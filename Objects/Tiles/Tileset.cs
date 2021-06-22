using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Objects;
using Pirita.Pools;
using System.Collections.Generic;

namespace Pirita.Tiles {
    public class Tileset : Drawable {
        protected Texture2D _texture { get; private set; } // Tileset spritesheet

        private Pool<Tile> _tilePool;
        public List<Tile> Tiles { get; private set; }

        // Tile size
        public override int Width => _texture.Width / 8;
        public override int Height => _texture.Height / 6;

        public Tileset(Texture2D texture) {
            _texture = texture;
            _tilePool = new Pool<Tile>(64);
            Tiles = new List<Tile>();
        }

        public void AddTile(int x, int y) {
            var tile = _tilePool.Get();
            tile.Position = new Vector2(x * Width, y * Height);

            if (Tiles.Find(t => t.Position == tile.Position) != null) return;

            Tiles.Add(tile);
            CheckConnections();
        }

        public void AddTiles(List<Vector2> positionList) {
            foreach (var p in positionList) {
                var tile = _tilePool.Get();
                tile.Position = new Vector2(p.X * Width, p.Y * Height);

                if (Tiles.Find(t => t.Position == tile.Position) != null) continue;

                Tiles.Add(tile);
            }
            CheckConnections();
        }

        /// <summary>Goes though each tile checking for other tiles next to each other to change its source position</summary>
        protected virtual void CheckConnections() {
            var middleOffset = new Vector2(Width, Height) / 2;

            foreach (var tile in Tiles) {
                Vector2 source = new Vector2(6, 5);
                var nearbyTiles = Tiles.FindAll(t => Vector2.Distance(tile.Position, t.Position) < Height * 2);

                if (nearbyTiles.Count <= 0) {
                    tile.Source = source * new Vector2(Width, Height);
                    continue;
                }

                bool topLeft, top, topRight,
                    left, right,
                    bottomLeft, bottom, bottomRight;

                topLeft = TileAt(-1, -1);
                top = TileAt(0, -1);
                topRight = TileAt(1, -1);

                left = TileAt(-1, 0);
                right = TileAt(1, 0);

                bottomLeft = TileAt(-1, 1);
                bottom = TileAt(0, 1);
                bottomRight = TileAt(1, 1);

                if (left) {
                    SetSource(5, 5);
                    if (top) {
                        SetSource(7, 4);
                        if (topLeft) {
                            SetSource(6, 4);
                            if (right) {
                                SetSource(6, 3);
                                if (topRight) {
                                    SetSource(4, 3);
                                    if (bottom) {
                                        SetSource(4, 1);
                                        if (bottomRight) {
                                            SetSource(0, 1);
                                            if (bottomLeft) {
                                                SetSource(0, 0);
                                            }
                                        } else if (bottomLeft) {
                                            SetSource(4, 0);
                                        }
                                    }
                                } else if (bottom) {
                                    SetSource(6, 1);
                                    if (bottomRight) {
                                        SetSource(2, 1);
                                        if (bottomLeft) {
                                            SetSource(2, 0);
                                        }
                                    } else if (bottomLeft) {
                                        SetSource(6, 0);
                                    }
                                }
                            } else if (bottom) {
                                SetSource(1, 3);
                                if (bottomLeft) {
                                    SetSource(0, 3);
                                }
                            }
                        } else if (right) {
                            SetSource(7, 3);
                            if (bottom) {
                                SetSource(7, 1);
                                if (bottomLeft) {
                                    SetSource(7, 0);
                                    if (topRight) {
                                        SetSource(5, 0);
                                        if (bottomRight) {
                                            SetSource(1, 0);
                                        } else if (topLeft) {
                                            SetSource(4, 0);
                                        }
                                    } else if (bottomRight) {
                                        SetSource(3, 0);
                                    }
                                } else if (topRight) {
                                    SetSource(5, 1);
                                    if (bottomRight) {
                                        SetSource(1, 1);
                                    }
                                } else if (bottomRight) {
                                    SetSource(3, 1);
                                }
                            } else if (topRight) {
                                SetSource(5, 3);
                            }
                        } else if (bottom) {
                            SetSource(3, 3);
                            if (bottomLeft) {
                                SetSource(2, 3);
                            }
                        }
                    } else if (bottom) {
                        SetSource(5, 4);
                        if (bottomLeft) {
                            SetSource(4, 4);
                            if (right) {
                                SetSource(5, 2);
                                if (bottomRight) {
                                    SetSource(4, 2);
                                }
                            }
                        } else if (right) {
                            SetSource(7, 2);
                            if (bottomRight) {
                                SetSource(6, 2);
                            }
                        }
                    } else if (right) {
                        SetSource(1, 4);
                    }
                } else if (right) {
                    SetSource(3, 5);
                    if (top) {
                        SetSource(1, 5);
                        if (topRight) {
                            SetSource(0, 5);
                            if (bottom) {
                                SetSource(2, 2);
                                if (bottomRight) {
                                    SetSource(0, 2);
                                }
                            }
                        } else if (bottom) {
                            SetSource(3, 2);
                            if (bottomRight) {
                                SetSource(1, 2);
                            }
                        }
                    } else if (bottom) {
                        SetSource(3, 4);
                        if (bottomRight) {
                            SetSource(2, 4);
                        }
                    }
                } else if (top) {
                    SetSource(4, 5);
                    if (bottom) {
                        SetSource(0, 4);
                    }
                } else if (bottom) {
                    SetSource(2, 5);
                }

                tile.Source = source * new Vector2(Width, Height);

                bool TileAt(int x, int y) {
                    var foundTile = nearbyTiles.Find(t => t.Position == new Vector2(tile.Position.X + (Width * x), tile.Position.Y + (Height * y)));
                    nearbyTiles.Remove(foundTile);

                    return foundTile != null;
                }

                void SetSource(int x, int y) {
                    source.X = x;
                    source.Y = y;
                }
            }

        }

        public override void Render(SpriteBatch spriteBatch) {
            foreach (var tile in Tiles) {
                spriteBatch.Draw(_texture, tile.Position, new Rectangle((int)tile.Source.X, (int)tile.Source.Y, Width, Height), Color.White);
            }
        }
    }
}
