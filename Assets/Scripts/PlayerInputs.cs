using Unity.Mathematics;


    public struct Inputs
    {
        public float2 moveInput;
        public bool jumpInput;
        public bool sprintInput;
        public bool crouchInput;
        public bool rotateLeft;
        public bool rotateRight;
        public bool shoot;
        public bool aim;
        public bool interact;
        public bool teleport;
        public byte hotbarSelection;

        //+X       = 1
        //-X       = 2
        //+Y       = 4
        //-Y       = 8
        //Jump     = 16
        //Sprint   = 32
        //Crouch   = 64
        //rotateL  = 128
        //rotateR  = 256
        //shoot    = 512
        //aim      = 1024
        //Interact = 2048
        //Teleport = 4096
        //hotbar   = 8192
        //hotbar   = 16,384
        //hotbar   = 32,768
        public ushort Pack()
        {
            ushort packed = 0;

            if (moveInput.x > 0)          packed |= 0x0001; // 1
            if (moveInput.x < 0)          packed |= 0x0002; // 2
            if (moveInput.y > 0)          packed |= 0x0004; // 4
            if (moveInput.y < 0)          packed |= 0x0008; // 8
            if (jumpInput)                packed |= 0x0010; // 16
            if (crouchInput)              packed |= 0x0040; // 64
            if (sprintInput)              packed |= 0x0020; // 32
            if (rotateLeft)               packed |= 0x0080; // 128
            if (rotateRight)              packed |= 0x0100; // 256
            if (shoot)                    packed |= 0x0200; // 512
            if (aim)                      packed |= 0x0400; // 1024
            if (interact)                 packed |= 0x0800; // 2048
            if (teleport)                 packed |= 0x1000; // 4096

            byte hotBar = hotbarSelection;

            if (hotBar >= 4) hotBar -= 4; packed |= 0x2000; // 8192
            if (hotBar >= 2) hotBar -= 2; packed |= 0x4000; // 16,384
            if (hotBar >= 1)              packed |= 0x8000; // 32,768


            return packed;
        }

        public void Unpack(ushort packed)
        {
            moveInput = float2.zero;
            hotbarSelection = 0;

            if ((packed & 0x0001) != 0) moveInput.x += 1;     // 1
            if ((packed & 0x0002) != 0) moveInput.x -= 1;     // 2
            if ((packed & 0x0004) != 0) moveInput.y += 1;     // 4
            if ((packed & 0x0008) != 0) moveInput.y -= 1;     // 8

            jumpInput = (packed & 0x0010) != 0;               // 16
            sprintInput = (packed & 0x0020) != 0;             // 32
            crouchInput = (packed & 0x0040) != 0;             // 64
            rotateLeft = (packed & 0x0080) != 0;              // 128
            rotateRight = (packed & 0x0100) != 0;             // 256
            shoot = (packed & 0x0200) != 0;                   // 512
            aim = (packed & 0x0400) != 0;                     // 1024
            interact = (packed & 0x0800) != 0;                // 2048
            teleport = (packed & 0x1000) != 0;                // 4096

            if ((packed & 0x2000) != 0) hotbarSelection += 1; // 8192
            if ((packed & 0x4000) != 0) hotbarSelection += 2; // 16384
            if ((packed & 0x8000) != 0) hotbarSelection += 4; // 32768
        }

        public static Inputs NewFromPacked(ushort packed)
        {
            Inputs playerInputs = new();
            playerInputs.Unpack(packed);
            return playerInputs;
        }
    }
